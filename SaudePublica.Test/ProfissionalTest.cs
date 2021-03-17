using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SaudePublica.Domain.Database;
using SaudePublica.Domain.Database.Entities;
using SaudePublica.Views;

namespace SaudePublica.Test
{
    [TestClass]
    public class ProfissionalTest
    {
        private ProfissionalsController controller;
        private Mock<DbSet<Profissional>> mockSetProfissional;
        private Mock<DbSet<Especialidade>> mockSetEspecialidade;
        private Mock<DbSet<Consultorio>> mockSetConsultorio;
        private Mock<DataContext> mockContext;

        [TestInitialize]
        public void Inicializar()
        {
            var dataProfissional = GetDataProfissional();
            var dataEspecialidade = GetDataEspecialidade();
            var dataConsultorio = GetDataConsultorio();

            mockSetProfissional = new Mock<DbSet<Profissional>>();
            mockSetProfissional.As<IQueryable<Profissional>>().Setup(m => m.Provider).Returns(dataProfissional.Provider);
            mockSetProfissional.As<IQueryable<Profissional>>().Setup(m => m.Expression).Returns(dataProfissional.Expression);
            mockSetProfissional.As<IQueryable<Profissional>>().Setup(m => m.ElementType).Returns(dataProfissional.ElementType);
            mockSetProfissional.As<IQueryable<Profissional>>().Setup(m => m.GetEnumerator()).Returns(dataProfissional.GetEnumerator());
            mockSetProfissional.Setup(x => x.Include("consultorios")).Returns(mockSetProfissional.Object);
            mockSetProfissional.Setup(x => x.Include("especialidade")).Returns(mockSetProfissional.Object);

            mockSetEspecialidade = new Mock<DbSet<Especialidade>>();
            mockSetEspecialidade.As<IQueryable<Especialidade>>().Setup(m => m.Provider).Returns(dataEspecialidade.Provider);
            mockSetEspecialidade.As<IQueryable<Especialidade>>().Setup(m => m.Expression).Returns(dataEspecialidade.Expression);
            mockSetEspecialidade.As<IQueryable<Especialidade>>().Setup(m => m.ElementType).Returns(dataEspecialidade.ElementType);
            mockSetEspecialidade.As<IQueryable<Especialidade>>().Setup(m => m.GetEnumerator()).Returns(dataEspecialidade.GetEnumerator());

            mockSetConsultorio = new Mock<DbSet<Consultorio>>();
            mockSetConsultorio.As<IQueryable<Consultorio>>().Setup(m => m.Provider).Returns(dataConsultorio.Provider);
            mockSetConsultorio.As<IQueryable<Consultorio>>().Setup(m => m.Expression).Returns(dataConsultorio.Expression);
            mockSetConsultorio.As<IQueryable<Consultorio>>().Setup(m => m.ElementType).Returns(dataConsultorio.ElementType);
            mockSetConsultorio.As<IQueryable<Consultorio>>().Setup(m => m.GetEnumerator()).Returns(dataConsultorio.GetEnumerator());

            mockContext = new Mock<DataContext>(MockBehavior.Loose);

            mockContext.SetupGet(x => x.Profissionais).Returns(mockSetProfissional.Object);
            mockContext.SetupGet(x => x.Consultorios).Returns(mockSetConsultorio.Object);
            mockContext.SetupGet(x => x.Especialidades).Returns(mockSetEspecialidade.Object);
          
            controller = new ProfissionalsController(mockContext.Object);
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
            Profissional _profissional = criarProfissional(1);
            Consultorio _consultorio = criarConsultorio(1);
            Especialidade _especialidade = criarEspecialidade(1);

            _profissional.consultorios.Add(_consultorio);
            _profissional.especialidade = _especialidade;

            var result = controller.Create(_profissional) as RedirectToRouteResult;

            mockSetProfissional.Verify(m => m.Add(It.IsAny<Profissional>()), Times.Once());
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
            Profissional profissional = criarProfissional(id);
            Consultorio consultorio = criarConsultorio(1);
            Especialidade especialidade = criarEspecialidade(1);

            profissional.consultorios.Add(consultorio);
            profissional.especialidade = especialidade;

            var result = controller.Edit(profissional) as RedirectToRouteResult;
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

        #region Private

        #region Pegar Dados

        private IQueryable<Profissional> GetDataProfissional()
        {
            return getListaProfissionais(3).AsQueryable();
        }

        private IQueryable<Especialidade> GetDataEspecialidade()
        {
            return getListaEspecialidades(3).AsQueryable();
        }

        private IQueryable<Consultorio> GetDataConsultorio()
        {
            return getListaConsultorios(3).AsQueryable();
        }

        #endregion

        #region Lista

        private List<Profissional> getListaProfissionais(int index)
        {
            var result = new List<Profissional>();

            for (int i = 0; i < index; i++)
                result.Add(criarProfissional(i));

            return result;
        }

        private List<Especialidade> getListaEspecialidades(int index)
        {
            var result = new List<Especialidade>();

            for (int i = 0; i < index; i++)
                result.Add(criarEspecialidade(i));

            return result;
        }

        private List<Consultorio> getListaConsultorios(int index)
        {
            var result = new List<Consultorio>();

            for (int i = 0; i < index; i++)
                result.Add(criarConsultorio(i));

            return result;
        }

        #endregion

        #region Criação

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

        private Especialidade criarEspecialidade(int index)
        {
            return new Especialidade
            {
                id = index,
                descricao = "Teste unitário " + index.ToString()
            };
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

        #endregion

        #endregion
    }
}
