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
            _repository = repository ?? throw new ArgumentNullException(nameof(repository)); 
        }

        [HttpGet]
        public async Task<ActionResult> GetKeyboards()
        {
            var results = await _repository.GetAllKeyboardAsync();

            return Ok(results);

        }


        [HttpGet("page")]
        public async Task<ActionResult> GetKeyboardByPage([FromQuery] int number, [FromQuery] int size)
        {
            var results = await _repository.GetKeyboardsByPageAsync(number, size);

            return Ok(results);

        }


        [HttpGet("{name}")]
        public async Task<ActionResult> GetKeyboardByName(string keyboardname)
        {
            var results = await _repository.GetKeyboardByName(keyboardname);

            if (results == null) return NotFound();

            return Ok(results);
        }



        [HttpGet("profile/{username}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetByUsername(string username)
        {
            var results = await _repository.GetKeyboardByUsernameAsync(username);

            if (results == null) return NotFound();

            return Ok(results);
        }



        [HttpGet("profile/{username}/page")]
        public async Task<ActionResult<KeyboardViewModel[]>> GetByUsername(string username, [FromQuery] int number, [FromQuery] int size)
        {
            var results = await _repository.GetKeyboardPageByUsernameAsync(number, size, username);

            if (results == null) return NotFound();

            return Ok(results);
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


        [HttpPatch]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<KeyboardViewModel[]>> PatchKeyboard([FromQuery] string currentusername, [FromBody] string username)
        {
            var oldKeyboard = await _repository.GetKeyboardByUsernameAsync(currentusername);

            if (oldKeyboard == null) return NotFound($"Could not find keyboards under the username {currentusername}");

            var patchedKeyboard = oldKeyboard.Select(result => result.ReplaceKeyboardUsername(username).ToKeyboardViewModel()).ToArray();
            await Task.WhenAll(_repository.SaveChangesAsync());

            return patchedKeyboard;

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