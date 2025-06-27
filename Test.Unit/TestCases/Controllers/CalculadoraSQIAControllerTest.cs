using API.Controllers;
using API.Models;
using API.Services;
using API.Services.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;

namespace Test.Unit.TestCases.Controllers
{
    public class CalculadoraSQIAControllerTest
    {
        private Mock<IInvestimentoPosFixadoService> _investimentoServiceMock;
        private Mock<ICotacaoService> _cotacaoServiceMock;
        private CalculadoraSQIAController _controllerSUT;

        public CalculadoraSQIAControllerTest()
        {
            var loggerMock = new Mock<ILogger<CalculadoraSQIAController>>();
            _investimentoServiceMock = new Mock<IInvestimentoPosFixadoService>();
            _cotacaoServiceMock = new Mock<ICotacaoService>();
            _controllerSUT = new CalculadoraSQIAController(loggerMock.Object, _investimentoServiceMock.Object, _cotacaoServiceMock.Object);
        }

        [Fact]
        public async Task DadoUmaDataDeAplicacaoPosteriorAFinal_AoValidar_UmaMensagemDeErroDeValicaoEhRetornada()
        {
            // Arrange
            var mensagemDeValidacaoEsperada = "não deve ser posterior";

            var dtAplicacao = new DateOnly(2026, 1, 1);
            var dtFinal = new DateOnly(2025, 1, 1);

            // Act
            var response = await _controllerSUT.AplicacaoAsync(100, dtAplicacao, dtFinal);

            // Assert
            var result = Assert.IsType<BadRequestObjectResult>(response);
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.Contains(mensagemDeValidacaoEsperada, result.Value.ToString());
        }

        [Fact]
        public async Task DadoParametrosValidos_AoCalcularUmaAplicacao_OFatorAcumuladoDeJurosEValorAtualizadoDoInvestimentoSaoRetornados()
        {
            // Arrange
            var aplicacao = 1000m;
            var dtAplicacao = new DateOnly(2025, 1, 1);
            var dtFinal = dtAplicacao;

            var cotacoes = new List<Cotacao>()
            {
                new Cotacao(){
                    Data=dtAplicacao.ToDateTime(TimeOnly.MinValue),
                    Valor=10
                }
            };
            _cotacaoServiceMock.Setup(x => x.GetIntervaloAsync(It.IsAny<DateOnly>(), It.IsAny<DateOnly>()))
                .ReturnsAsync(cotacoes)
                .Verifiable(Times.Once);

            var aplicacaoCalculada = new AplicacaoCalculada()
            {
                ValorAtualizadoDoInvestimento = aplicacao
            };
            _investimentoServiceMock.Setup(x => x.CalcularBaseadoNosIndices(It.IsAny<decimal>(), It.IsAny<IEnumerable<Cotacao>>()))
                .Returns(aplicacaoCalculada)
                .Verifiable(Times.Once);

            // Act
            var response = await _controllerSUT.AplicacaoAsync(100, dtAplicacao, dtFinal);

            // Assert
            _cotacaoServiceMock.VerifyAll();
            _investimentoServiceMock.VerifyAll();
            var result = Assert.IsType<OkObjectResult>(response);
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            var content = result.Value as AplicacaoCalculada;
            Assert.NotNull(content);
            Assert.Equal(1, content.FatorAcumuladoDeJuros);
            Assert.Equal(aplicacao, content.ValorAtualizadoDoInvestimento);
        }
    }
}
