using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MechKeyboardBase.Web.Data;
using MechKeyboardBase.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace MechKeyboardBase.Web.Controllers
{   

    [Route("api/[controller]")]
    [ApiController]
    public class KeyboardController : ControllerBase
    {
        private readonly IMechKeyboardRepository _repository;
        private readonly LinkGenerator _linkGenerator;

        public KeyboardController(IMechKeyboardRepository repository, LinkGenerator linkGenerator)
        {
            _repository = repository;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<Keyboard[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllKeyboardsAsync();

                return Ok(results.Select(result => result.ToKeyboardViewModel()).ToArray());

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Keyboard>> Get(int id)
        {
            try
            {
                var results = await _repository.GetKeyboardByIdAsync(id);

                return Ok(results.ToKeyboardViewModel());

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Keyboard>> Get(string name)
        {
            try
            {
                var results = await _repository.GetKeyboardByNameAsync(name);

                return Ok(results.ToKeyboardViewModel());

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }


        [HttpPost]
        public async Task<ActionResult<Keyboard>> Post(Keyboard keyboard)
        {
            try
            {
                var newKeyboard = keyboard.ToKeyboardModel();

                _repository.Add(newKeyboard);

                if(await _repository.SaveChangesAsync())
                {
                    return Ok(newKeyboard);
                }

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }


    }
}