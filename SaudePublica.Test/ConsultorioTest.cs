using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SaudePublica.Controllers;
using SaudePublica.Domain.Database;
using SaudePublica.Domain.Database.Entities;

namespace SaudePublica.Test
{
    [TestClass]
    public class ConsultorioTest
    {
        private ConsultoriosController controller;
        private Mock<DbSet<Consultorio>> mockSet;
        private Mock<DbSet<Profissional>> mockSetProfissional;
        private Mock<DataContext> mockContext;

        [TestInitialize]
        public void Inicializar()
        {
            var data = GetData();
            var dataProfissional = GetDataProfissional();

            mockSet = new Mock<DbSet<Consultorio>>();
            mockSet.As<IQueryable<Consultorio>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Consultorio>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Consultorio>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Consultorio>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockSet.Setup(x=> x.Include("profissionals")).Returns(mockSet.Object);

            mockSetProfissional = new Mock<DbSet<Profissional>>();
            mockSetProfissional.As<IQueryable<Profissional>>().Setup(m => m.Provider).Returns(dataProfissional.Provider);
            mockSetProfissional.As<IQueryable<Profissional>>().Setup(m => m.Expression).Returns(dataProfissional.Expression);
            mockSetProfissional.As<IQueryable<Profissional>>().Setup(m => m.ElementType).Returns(dataProfissional.ElementType);
            mockSetProfissional.As<IQueryable<Profissional>>().Setup(m => m.GetEnumerator()).Returns(dataProfissional.GetEnumerator());

            mockContext = new Mock<DataContext>(MockBehavior.Loose);

            mockContext.SetupGet(x => x.Consultorios).Returns(mockSet.Object);
            mockContext.SetupGet(x => x.Profissionais).Returns(mockSetProfissional.Object);

            controller = new ConsultoriosController(mockContext.Object);
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
            Assert.IsNotNull(result.Model, "Erro ao carregar a lista de Consultorio");
        }

        [TestMethod]
        public void Criar()
        {
            Consultorio Consultorio = criarConsultorio(0);

            var result = controller.Create(Consultorio) as RedirectToRouteResult;

            mockSet.Verify(m => m.Add(It.IsAny<Consultorio>()), Times.Once());
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
        [DataRow(1)]
        public void EditarPost(int id)
        {
            Consultorio Consultorio = criarConsultorio(id);

            var result = controller.Edit(Consultorio) as RedirectToRouteResult;
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

        [TestMethod]
        public void Report()
        {
            var result = controller.Report() as ViewResult;
            Assert.IsNotNull(result);
        }

        #region private

        private IQueryable<Consultorio> GetData()
        {
            return new List<Consultorio>
            {
                criarConsultorio(1),
                criarConsultorio(2),
                criarConsultorio(3)
            }.AsQueryable();
        }

        private IQueryable<Profissional> GetDataProfissional()
        {
            return getListaProfissionais(3).AsQueryable();
        }

        private Consultorio criarConsultorio(int index)
        {
            return new Consultorio
            {
                id = index,
                nome = "Teste unitário " + index.ToString(),
                numeroSala = 123,
                andar = 1,
                setor = "Principal",
                latitude = "123456789",
                longitude = "123456789"
            };
        }

        private List<Profissional> getListaProfissionais(int index)
        {
            var result = new List<Profissional>();

            for (int i = 0; i < index; i++)
                result.Add(criarProfissional(i));

            return result;
        }

        private Profissional criarProfissional(int index)
        {
            return new Profissional
            {
                id = index,
                nome = "Teste unitário " + index.ToString(),
                funcao = "Função",
                registroProfissional = "Registro",
                cpf = "123.123.123/12",
                dataNascimento = DateTime.Now,
                idEspecialidade = 1,
                sexo = 0,
                cep = "12345-678",
                logradouro = "Rua",
                numeroLogradouro = 123,
                complemento = "Complemento",
                bairro = "Bairro",
                telefone = "123456789",
                celular = "123456789",
                email = "teste@teste.com.br",
                statusProfissional = "Status"
            };
        }

        #endregion
    }
}