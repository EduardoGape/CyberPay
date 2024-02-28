using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Application.IServices;
using Domain.Entities;
using Microsoft.AspNetCore.Cors;
using Domain.Filters;




namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        /// <summary>
        /// Profile - Registro
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        public IActionResult Register([FromBody] Profile profile)
        {
            var result = _profileService.Register(profile);
            return Ok(result);
        }

        /// <summary>
        /// Profile - Atualizar
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
        public IActionResult Update([FromRoute] string id, [FromBody] Profile profile)
        {
            var result = _profileService.Update(id, profile);
            return Ok(result);
        }

        /// <summary>
        /// Profile - Buscar por id
        /// </summary>
        /// <response code="200">Returns success</response>
        /// <response code="400">Custom Error</response>
        /// <response code="401">Unauthorize Error</response>
        /// <response code="500">Exception Error</response>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Profile), 200)]
        [ProducesResponseType(typeof(Profile), 400)]
        [ProducesResponseType(typeof(Profile), 401)]
        [ProducesResponseType(typeof(Profile), 500)]
        public IActionResult GetById([FromRoute] string id)
        {
            var result = _profileService.GetById(id);
            return Ok(result);
        }

        /// <summary>
        /// Profile - Listar todos com filtro
        /// </summary>
        /// <response code="200">Returns success</response>
        /// <response code="400">Custom Error</response>
        /// <response code="401">Unauthorize Error</response>
        /// <response code="500">Exception Error</response>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Profile), 200)]
        [ProducesResponseType(typeof(Profile), 400)]
        [ProducesResponseType(typeof(Profile), 401)]
        [ProducesResponseType(typeof(Profile), 500)]
        public IActionResult GetAll([FromBody] ProfileFilter Filter)
        {
            var result = _profileService.GetAll(Filter);
            return Ok(result);
        }

    }
}
