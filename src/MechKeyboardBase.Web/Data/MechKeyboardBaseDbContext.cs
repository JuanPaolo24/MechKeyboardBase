using MechKeyboardBase.Web.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MechKeyboardBase.Web.Data
{
    public class MechKeyboardBaseDbContext : DbContext
    {

        public MechKeyboardBaseDbContext(DbContextOptions<MechKeyboardBaseDbContext> options) : base(options)
        {

        }
        public DbSet<Keyboard> Keyboard { get; set; }
        public DbSet<KeyboardBuild> KeyboardBuild { get; set; }


    }
}
