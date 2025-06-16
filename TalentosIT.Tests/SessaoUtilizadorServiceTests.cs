using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TalentosIT.Services;
using TalentosIT.Data;
using TalentosIT.Models;
using TalentosIT.Tests.Mocks;

namespace TalentosIT.Tests
{
    [TestFixture]
    public class SessaoUtilizadorServiceTests
    {
        private SessaoUtilizadorService _service;
        private ApplicationDbContext _context;
        private FakeSession _session;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDB")
                .Options;

            _context = new ApplicationDbContext(options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _context.Utilizadores.Add(new Utilizador
            {
                Id = 1,
                Nome = "Teste",
                Email = "teste@teste.com",
                Password = "123456",   
                Tipo = "Cliente"     
            });

            _context.SaveChanges();

            _session = new FakeSession();
            _session.SetInt32("UserId", 1);
            _session.SetString("UserTipo", "Cliente");

            var httpContext = new DefaultHttpContext
            {
                Session = _session
            };

            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            httpContextAccessor.Setup(a => a.HttpContext).Returns(httpContext);

            _service = new SessaoUtilizadorService(httpContextAccessor.Object, _context);
        }


        [Test]
        public void ObterIdUtilizador_DeveRetornarId()
        {
            var result = _service.ObterIdUtilizador();
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void ObterUtilizador_DeveRetornarUtilizadorValido()
        {
            var result = _service.ObterUtilizador();
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Nome, Is.EqualTo("Teste"));
        }

        [Test]
        public void ObterTipoUtilizador_DeveRetornarTipo()
        {
            var result = _service.ObterTipoUtilizador();
            Assert.That(result, Is.EqualTo("Cliente"));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }
    }
}
