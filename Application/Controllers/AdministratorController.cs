using Application.IServices;
using Domain.Entities;
using Domain.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdministratorController : ControllerBase
    {
        private readonly IAdministratorService _administratorService;

        public AdministratorController(IAdministratorService administratorService)
        {
            _administratorService = administratorService;
        }

        /// <summary>
        /// Profile - Registro
        /// </summary>
        /// <response code="200">Returns success</response>
        /// <response code="400">Custom Error</response>
        /// <response code="401">Unauthorize Error</response>
        /// <response code="500">Exception Error</response>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 401)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Register([FromBody] Administrator Administrator)
        {
            var result = _administratorService.Register(Administrator);
            return Ok(result);
        }

        /// <summary>
        /// Administrator - Atualizar
        /// </summary>
        /// <response code="200">Returns success</response>
        /// <response code="400">Custom Error</response>
        /// <response code="401">Unauthorize Error</response>
        /// <response code="500">Exception Error</response>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 401)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Update([FromRoute] string id, [FromBody] Administrator Administrator)
        {
            var result = _administratorService.Update(id, Administrator);
            return Ok(result);
        }

        /// <summary>
        /// Administrator - Buscar por id
        /// </summary>
        /// <response code="200">Returns success</response>
        /// <response code="400">Custom Error</response>
        /// <response code="401">Unauthorize Error</response>
        /// <response code="500">Exception Error</response>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Administrator), 200)]
        [ProducesResponseType(typeof(Administrator), 400)]
        [ProducesResponseType(typeof(Administrator), 401)]
        [ProducesResponseType(typeof(Administrator), 500)]
        public IActionResult GetById([FromRoute] string id)
        {
            var result = _administratorService.GetById(id);
            return Ok(result);
        }

        /// <summary>
        /// Administrator - Listar todos com filtro
        /// </summary>
        /// <response code="200">Returns success</response>
        /// <response code="400">Custom Error</response>
        /// <response code="401">Unauthorize Error</response>
        /// <response code="500">Exception Error</response>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Administrator), 200)]
        [ProducesResponseType(typeof(Administrator), 400)]
        [ProducesResponseType(typeof(Administrator), 401)]
        [ProducesResponseType(typeof(Administrator), 500)]
        public IActionResult GetAll([FromBody] AdministratorFilter Filter)
        {
            var result = _administratorService.GetAll(Filter);
            return Ok(result);
        }
        

    }
}
