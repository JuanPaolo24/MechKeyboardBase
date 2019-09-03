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

        public KeyboardController(IKeyboardRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Keyboard[]>> Get()
        {
            var results = await _repository.GetAllKeyboardsAsync();

            return results.Select(result => result.ToKeyboardViewModel()).ToArray();

        }


        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Keyboard>> Get(string name)
        {
            var results = await _repository.GetKeyboardByNameAsync(name);

            return results.ToKeyboardViewModel();
        }

        [HttpGet("details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Keyboard[]>> FilterKeyboardSearch([FromQuery] string caseName = null,
                                                                         [FromQuery] string pcb = null,
                                                                         [FromQuery] string plate = null,
                                                                         [FromQuery] string keycaps = null,
                                                                         [FromQuery] string switchName = null)
        {

                var keyboardFilters = new KeyboardBuild()
                {
                    Case = caseName,
                    PCB = pcb,
                    Plate = plate,
                    Keycaps = keycaps,
                    Switch = switchName
                };

                var results = await _repository.GetKeyboardByKeyboardDetails(keyboardFilters.ToKeyboardBuildModel());

                return results[].ToKeyboardViewModel();
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Keyboard>> PostKeyboard(Keyboard keyboard)
        {

            var newKeyboard = keyboard.ToKeyboardModel();

            if (keyboard == null) return BadRequest();

            _repository.Add(newKeyboard);
            await _repository.SaveChangesAsync();
            return newKeyboard.ToKeyboardViewModel();
        }


        [HttpPut("{name}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Keyboard>> Put(string name, Keyboard keyboard)
        {

            var oldKeyboard = await _repository.GetKeyboardByNameAsync(name);
            if (oldKeyboard == null) return NotFound($"Could not find a keyboard with name of {name}");

            oldKeyboard.ReplaceKeyboard(keyboard);
            await _repository.SaveChangesAsync();

            return oldKeyboard.ToKeyboardViewModel();
        }


        [HttpDelete("{name}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Keyboard>> Delete(string name)
        {

            var oldKeyboard = await _repository.GetKeyboardByNameAsync(name);
            if (oldKeyboard == null) return NotFound($"Could not find a keyboard with name of {name}");

            _repository.Delete(oldKeyboard);
            _repository.Delete(oldKeyboard.KeyboardDetails);
            await _repository.SaveChangesAsync();
            return Accepted(oldKeyboard.Name + " deleted");
        }


    }
}