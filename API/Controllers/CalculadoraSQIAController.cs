using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculadoraSQIAController : Controller
    {
        private ILogger<CalculadoraSQIAController> _logger;
        private IInvestimentoPosFixadoService _investimentoPosFixadoService;
        private ICotacaoService _cotacaoService;

        public CalculadoraSQIAController(ILogger<CalculadoraSQIAController> logger, IInvestimentoPosFixadoService investimentoPosFixadoService, ICotacaoService cotacaoService)
        {
            _logger = logger;
            _investimentoPosFixadoService = investimentoPosFixadoService;
            _cotacaoService = cotacaoService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> AplicacaoAsync(decimal valorInvestido, DateOnly dataAplicacao, DateOnly dataFinal)
        {
            _logger.LogInformation("Requisitado AplicacaoAsync (valorInvestido:{valorInvestido}, dataAplicacao:{dataAplicacao}, dataFinal:{dataFinal})", valorInvestido, dataAplicacao, dataFinal);

            if (dataAplicacao > dataFinal)
            {
                var validacao = $"Data de aplicação ({dataAplicacao}) não deve ser posterior a data Final ({dataFinal}).";
                _logger.LogInformation(validacao);
                return BadRequest(validacao);
            }

            try
            {
                var cotacoes = await _cotacaoService.GetIntervaloAsync(dataAplicacao, dataFinal.AddDays(-1));
                var aplicacao = _investimentoPosFixadoService.CalcularBaseadoNosIndices(valorInvestido, cotacoes);

                return Ok(aplicacao);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro em AplicacaoAsync ({valorInvestido}, {dataAplicacao}, {dataFinal}): {ex.Message}");
                return StatusCode(500, "Ocorreu um erro ao tentar calcular a aplicação.");
            }
        }
    }
}
