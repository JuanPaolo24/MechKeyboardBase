using MechKeyboardBase.Web.Data;
using MechKeyboardBase.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MechKeyboardBase.Web.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class KeyboardController : ControllerBase
    {
        private readonly IKeyboardRepository _repository;
        private readonly LinkGenerator _linkGenerator;

        public KeyboardController(IKeyboardRepository repository, LinkGenerator linkGenerator)
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

        [HttpGet("details")]
        public async Task<ActionResult<Keyboard[]>> Get([FromQuery]KeyboardBuild keyboard)
        {
            try
            {
                var results = await _repository.GetKeyboardByKeyboardDetails(keyboard.ToKeyboardBuildModel());

                return Ok(results.Select(result => result.ToKeyboardViewModel()).ToArray());
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest();
        }


        [HttpPost]
        public async Task<ActionResult<Keyboard>> Post(Keyboard keyboard)
        {
            try
            {
                var newKeyboard = keyboard.ToKeyboardModel();

                _repository.Add(newKeyboard);

                if (await _repository.SaveChangesAsync())
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


        [HttpPut("{name}")]
        public async Task<ActionResult<Keyboard>> Put(string name, Keyboard keyboard)
        {
            try
            {
                var oldKeyboard = await _repository.GetKeyboardByNameAsync(name);
                if (oldKeyboard == null) return NotFound($"Could not find a keyboard with name of {name}");

                oldKeyboard.ReplaceKeyboard(keyboard);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok(oldKeyboard.ToKeyboardViewModel());
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest();
        }


        [HttpDelete("{name}")]
        public async Task<ActionResult<Keyboard>> Delete(string name)
        {
            try
            {
                var oldKeyboard = await _repository.GetKeyboardByNameAsync(name);
                if (oldKeyboard == null) return NotFound();


                _repository.Delete(oldKeyboard);
                _repository.Delete(oldKeyboard.KeyboardDetails);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok(oldKeyboard.Name + " deleted");
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