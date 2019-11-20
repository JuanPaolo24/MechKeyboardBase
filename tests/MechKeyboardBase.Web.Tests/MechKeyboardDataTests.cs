using MechKeyboardBase.Core.Entities;
using MechKeyboardBase.Infrastructure;
using MechKeyboardBase.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Microsoft.EntityFrameworkCore.InMemory;
using System.Linq;
using MechKeyboardBase.Infrastructure.Helpers;
using System.Threading.Tasks;

namespace MechKeyboardBase.Web.Tests
{
    public class MechKeyboardDataTests
    {

        [Fact]
        public void Paginator_CheckPaging_ReturnsthecorrectnumberofitemsAsync()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MechKeyboardBaseDbContext>()
                .UseInMemoryDatabase("KeyboardInMemoryDB")
                .Options;

            using (var context = new MechKeyboardBaseDbContext(options))
            {
                context.Keyboard.Add(new Keyboard()
                {
                    KeyboardId = 7,
                    Username = "user7",
                    KeyboardName = "Test1d",
                    Case = "test",
                    PCB = "test",
                    Plate = "test",
                    Keycaps = "EPBT test",
                    Switch = "test Inks"

                });

                context.Keyboard.Add(new Keyboard()
                {
                    KeyboardId = 8,
                    Username = "user7",
                    KeyboardName = "Test1d",
                    Case = "test",
                    PCB = "test",
                    Plate = "test",
                    Keycaps = "EPBT test",
                    Switch = "test Inks"

                });

                context.Keyboard.Add(new Keyboard()
                {
                    KeyboardId = 9,
                    Username = "user7",
                    KeyboardName = "Test1d",
                    Case = "test",
                    PCB = "test",
                    Plate = "test",
                    Keycaps = "EPBT test",
                    Switch = "test Inks"

                });

                context.Keyboard.Add(new Keyboard()
                {
                    KeyboardId = 10,
                    Username = "user7",
                    KeyboardName = "Test1d",
                    Case = "test",
                    PCB = "test",
                    Plate = "test",
                    Keycaps = "EPBT test",
                    Switch = "test Inks"

                });

                context.Keyboard.Add(new Keyboard()
                {
                    KeyboardId = 11,
                    Username = "user7",
                    KeyboardName = "Test1d",
                    Case = "test",
                    PCB = "test",
                    Plate = "test",
                    Keycaps = "EPBT test",
                    Switch = "test Inks"

                });

                context.SaveChanges();

                var keyboardRepository = new KeyboardRepository(context);

                //Act

                var listQuery = keyboardRepository.GetKeyboardsByPageAsync(2, 1);

                //Assert

                Assert.Single(listQuery.Result);
            }
        }

    }
}
