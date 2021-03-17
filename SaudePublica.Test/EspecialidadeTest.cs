using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SaudePublica.Domain.Database;
using SaudePublica.Domain.Database.Entities;
using SaudePublica.Views.Doctor;
using RedirectToRouteResult = System.Web.Mvc.RedirectToRouteResult;

namespace SaudePublica.Test
{
    [TestClass]
    public class EspecialidadeTest
    {
        private EspecialidadesController controller;
        private Mock<DbSet<Especialidade>> mockSet;
        private Mock<DataContext> mockContext;

        [TestInitialize]
        public void Inicializar()
        {
            var data = GetData();

            mockSet = new Mock<DbSet<Especialidade>>();
            mockSet.As<IQueryable<Especialidade>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Especialidade>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Especialidade>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Especialidade>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext = new Mock<DataContext>();

            mockContext.Setup(x => x.Especialidades)
                .Returns(mockSet.Object);

            controller = new EspecialidadesController(mockContext.Object);
        }

        [TestCleanup]
        public void Clear()
        {
            controller.Dispose();
        }

        [TestMethod]
        public void AbrirPaginaIndex()
        {
            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result, "Erro ao carregar a página inicial");
            Assert.IsNotNull(result.Model, "Erro ao carregar a lista de especialidades");
        }

        [TestMethod]
        [DataRow("Teste unitário")]
        public void Criar(string descricao)
        {
            Especialidade especialidade = new Especialidade
            {
                descricao = descricao
            };

            var result = controller.Create(especialidade) as RedirectToRouteResult;

            mockSet.Verify(m => m.Add(It.IsAny<Especialidade>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        [DataRow(1)]
        public void Detalhar(int id)
        {
            var result = controller.Details(id) as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [DataRow(1)]
        public void EditarGet(int id)
        {
            var result = controller.Edit(id) as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        [DataRow(1, "Editar - Teste Unitário")]
        public void EditarPost(int id, string descricao)
        {
            Especialidade especialidade = new Especialidade
            {
                id = id,
                descricao = descricao
            };

            var result = controller.Edit(especialidade) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        [DataRow(1)]
        public void Deletar(int id)
        {
            ViewResult result = controller.Delete(id) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        [DataRow(1)]
        public void DeletarConfirmacao(int id)
        {
            var result = controller.DeleteConfirmed(id) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        #region private

        private IQueryable<Especialidade> GetData()
        {
            return new List<Especialidade>
            {
                new Especialidade { id = 1, descricao = "Especialidade 1" },
                new Especialidade { id = 2, descricao = "Especialidade 2" },
                new Especialidade { id = 3, descricao = "Especialidade 3" }
            }.AsQueryable();
        }

        #endregion
    }
}
