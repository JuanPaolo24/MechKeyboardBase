using MechKeyboardBase.Core.Entities;
using MechKeyboardBase.Infrastructure.Repositories;
using MechKeyboardBase.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MechKeyboardBase.Web.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class KeyboardController : ControllerBase
    {
        private readonly IKeyboardRepository _repository;

        public KeyboardController(IKeyboardRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<KeyboardViewModel[]>> Get()
        {
            var results = await _repository.GetAllKeyboardAsync();

            return results.Select(result => result.ToKeyboardViewModel()).ToArray();

        }


        [HttpGet("page")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<KeyboardViewModel[]>> Get([FromQuery] int number, [FromQuery] int size)
        {
            var results = await _repository.GetKeyboardsByPageAsync(number, size);

            return results.Select(result => result.ToKeyboardViewModel()).ToArray();

        }


        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<KeyboardViewModel[]>> Get(string keyboardname)
        {
            var keyboardFilters = new Keyboard()
            {
                KeyboardName = keyboardname
            };
    
            var results = await _repository.FilterKeyboardsAsync(keyboardFilters);

            return results.Select(result => result.ToKeyboardViewModel()).ToArray();
        }



        [HttpGet("userprofile")]
        public async Task<ActionResult<KeyboardViewModel[]>> GetByUsername([FromQuery] string username)
        {
            var results = await _repository.GetKeyboardByUsernameAsync(username);

            return results.Select(result => result.ToKeyboardViewModel()).ToArray();
        }



        [HttpGet("userprofile/page")]
        public async Task<ActionResult<KeyboardViewModel[]>> GetByUsername([FromQuery] string username, [FromQuery] int number, [FromQuery] int size)
        {
            var results = await _repository.GetKeyboardByUsernamePageAsync(number, size, username);

            return results.Select(result => result.ToKeyboardViewModel()).ToArray();
        }


        [HttpGet("details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<KeyboardViewModel[]>> FilterKeyboardSearch([FromQuery] string caseName = null,
                                                                         [FromQuery] string pcb = null,
                                                                         [FromQuery] string plate = null,
                                                                         [FromQuery] string keycaps = null,
                                                                         [FromQuery] string switchName = null)
        {

                var keyboardFilters = new Keyboard()
                {
                    Case = caseName,
                    PCB = pcb,
                    Plate = plate,
                    Keycaps = keycaps,
                    Switch = switchName
                };

                var results = await _repository.FilterKeyboardsAsync(keyboardFilters);


                return results.Select(result => result.ToKeyboardViewModel()).ToArray();
        }



        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<KeyboardViewModel>> PostKeyboard([FromBody] KeyboardViewModel keyboard)
        {

            keyboard.Username = User.FindFirstValue(ClaimTypes.UserData);

            var newKeyboard = keyboard.ToKeyboardModel();

            if (keyboard == null) return NoContent();

            _repository.Add(newKeyboard);
            await _repository.SaveChangesAsync();
            return newKeyboard.ToKeyboardViewModel();
        }


        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<KeyboardViewModel>> ReplaceKeyboard([FromQuery] string name, [FromBody] KeyboardViewModel keyboard)
        {
            var currentUsername = User.FindFirstValue(ClaimTypes.UserData);
            var oldKeyboard = await _repository.GetKeyboardByNameAndUsernameAsync(name, currentUsername);

            if (oldKeyboard == null) return NotFound($"Could not find a keyboard with name of {name}");

            oldKeyboard.ReplaceKeyboard(keyboard);
            await _repository.SaveChangesAsync();

            return oldKeyboard.ToKeyboardViewModel();
        }


        [HttpDelete]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Keyboard>> DeleteKeyboard([FromQuery] string keyboardname)
        {
            var currentUsername = User.FindFirstValue(ClaimTypes.UserData);
            var oldKeyboard = await _repository.GetKeyboardByNameAndUsernameAsync(keyboardname, currentUsername);
                   
            if (oldKeyboard == null) return NotFound($"Could not find a keyboard with name of {keyboardname}");

            _repository.Delete(oldKeyboard);
            await _repository.SaveChangesAsync();
            return Accepted(oldKeyboard.KeyboardName + " deleted");
        }


    }
}