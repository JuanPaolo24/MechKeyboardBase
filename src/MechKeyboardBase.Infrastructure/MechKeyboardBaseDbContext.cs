using MechKeyboardBase.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MechKeyboardBase.Infrastructure
{
    public class MechKeyboardBaseDbContext : DbContext
    {
        public MechKeyboardBaseDbContext(DbContextOptions<MechKeyboardBaseDbContext> options) : base(options)
        {
           
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Keyboard> Keyboard { get; set; }
        public DbSet<KeyboardDetails> KeyboardDetails { get; set; }
    }
}
