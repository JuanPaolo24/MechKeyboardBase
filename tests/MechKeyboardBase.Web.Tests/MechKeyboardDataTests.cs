using MechKeyboardBase.Web.Data;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace MechKeyboardBase.Web.Tests
{
    public class MechKeyboardDataTests
    {
        [Fact]
        public void GetKeyboardByKeyboardDetails()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MechKeyboardBaseDbContext>()
                .Options;

            using (var context = new MechKeyboardBaseDbContext(options))
            {
                context.Keyboard.Add(new Models.Keyboard()
                {
                    Name = "Silent Assassin",
                    Inspiration = "Inpired from the Ninja Assassin movie",
                    KeyboardDetails = new Models.KeyboardBuild()
                    {
                        Case = "E6.5 Poly Carb",
                        PCB = "E6.5 PCB",
                        Plate = "Poly Carb",
                        Keycaps = "GMK Minimal",
                        Switch = "Healios"
                    }

                }); ;

                context.SaveChanges();

                var keyboard = new Models.KeyboardBuild()
                {
                    Case = "E6.5 Poly Carb",
                    PCB = "E6.5 PCB",
                    Plate = "Poly Carb",
                    Keycaps = "GMK Minimal",
                    Switch = "Healios"
                };

                var repository = new KeyboardRepository(context);

                //Act
                var checkGet = repository.GetKeyboardByKeyboardDetails(keyboard);

                //Assert
                Assert.Equal(keyboard.ToString(), checkGet.ToString());
            }
        }
    }
}
