using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApiPruebaInalambria.Backend;
using WebApiPruebaInalambria.Model;
namespace WebApiPruebaInalambria.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class Prueba : ControllerBase
    {
        private readonly Converter _converter;
        private readonly ILogger<Prueba> _logger;

        public Prueba(ILogger<Prueba> logger, Converter convert)
        {
            _logger = logger;
            _converter = convert;
        }
        [HttpPost("NumberToWords")]
        public IActionResult NumberToWords([FromBody] Request request)
        {
            //Valida que el request o input no sea nulo
            if (request == null || !ModelState.IsValid)
            {
                return BadRequest("Solicitud no válida");
            }
            var response = _converter.ConverterIntToString(request);
            return Ok(response);
        }
        
    }
}
