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
        public void FilterKeyboardAsync_CheckOneParameter_Returns2()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MechKeyboardBaseDbContext>()
                .UseInMemoryDatabase("KeyboardInMemoryDB")
                .Options;

            using (var context = new MechKeyboardBaseDbContext(options))
            {
                context.Keyboard.Add(new Keyboard()
                {
                    KeyboardId = 1,
                    Username = "user1",
                    KeyboardName = "Test1",
                    Case = "KBD67",
                    PCB = "KBD65",
                    Plate = "Aluminium",
                    Keycaps = "EPBT 9009",
                    Switch = "Gateron Inks"

                });

                context.Keyboard.Add(new Keyboard()
                {
                    KeyboardId = 2,
                    Username = "user2",
                    KeyboardName = "Test2",
                    Case = "TGR Alice",
                    PCB = "TGR Alice PCB",
                    Plate = "Aluminium",
                    Keycaps = "GMK Jamon",
                    Switch = "Retooled MX Blacks"

                });

                context.Keyboard.Add(new Keyboard()
                {
                    KeyboardId = 3,
                    Username = "user3",
                    KeyboardName = "Test3",
                    Case = "KBD67",
                    PCB = "KBD65",
                    Plate = "Brass",
                    Keycaps = "GMK Olivia",
                    Switch = "Gateron Inks"

                });

                context.SaveChanges();

                var keyboardRepository = new KeyboardRepository(context);

                var filter = new Keyboard()
                {
                    Case = "KBD67"
                };


                //Act
                var checkFilter = keyboardRepository.FilterKeyboardsAsync(filter);


                //Assert
                Assert.Equal(2, checkFilter.Result.Length);
            }
        }


        [Fact]
        public void FilterKeyboardAsync_CheckTwoParameters_Returns1()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MechKeyboardBaseDbContext>()
                .UseInMemoryDatabase("KeyboardInMemoryDB")
                .Options;

            using (var context = new MechKeyboardBaseDbContext(options))
            {
                context.Keyboard.Add(new Keyboard()
                {
                    KeyboardId = 4,
                    Username = "user1",
                    KeyboardName = "Test1",
                    Case = "E6.5",
                    PCB = "KBD65",
                    Plate = "Aluminium",
                    Keycaps = "EPBT 9009",
                    Switch = "Gateron Inks"

                });

                context.Keyboard.Add(new Keyboard()
                {
                    KeyboardId = 5,
                    Username = "user2",
                    KeyboardName = "Test2",
                    Case = "TGR Alice",
                    PCB = "TGR Alice PCB",
                    Plate = "Aluminium",
                    Keycaps = "GMK Jamon",
                    Switch = "Retooled MX Blacks"

                });

                context.Keyboard.Add(new Keyboard()
                {
                    KeyboardId = 6,
                    Username = "user3",
                    KeyboardName = "Test3",
                    Case = "E6.5",
                    PCB = "KBD65",
                    Plate = "Brass",
                    Keycaps = "GMK Olivia",
                    Switch = "Gateron Inks"

                });

                context.SaveChanges();

                var keyboardRepository = new KeyboardRepository(context);

                var filter = new Keyboard()
                {
                    Case = "E6.5",
                    Plate = "Brass"
                };


                //Act
                var checkFilter = keyboardRepository.FilterKeyboardsAsync(filter);


                //Assert
                Assert.Single(checkFilter.Result);
            }
        }


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

                var listQuery = keyboardRepository.GetKeyboardByPageAsync(2, 1);

                //Assert

                Assert.Single(listQuery.Result);
            }
        }

    }
}
