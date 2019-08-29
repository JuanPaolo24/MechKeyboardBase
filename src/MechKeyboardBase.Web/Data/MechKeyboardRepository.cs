using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MechKeyboardBase.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace MechKeyboardBase.Web.Data
{
    public class MechKeyboardRepository : IMechKeyboardRepository
    {
        private readonly MechKeyboardBaseDbContext _context;

        public MechKeyboardRepository(MechKeyboardBaseDbContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Keyboard[]> GetAllKeyboardsAsync()
        {
            IQueryable<Keyboard> query = _context.Keyboard;

            query = query
                 .Include(c => c.KeyboardDetails);

            return await query.ToArrayAsync();
        }


        public async Task<Keyboard> GetKeyboardByIdAsync(int id)
        {
            IQueryable<Keyboard> query = _context.Keyboard;

            query = query
                .Include(c => c.KeyboardDetails)
                .Where(t => t.ID == id);

            return await query.FirstOrDefaultAsync();

        }

        public async Task<Keyboard> GetKeyboardByNameAsync(string name)
        {
            IQueryable<Keyboard> query = _context.Keyboard;

            query = query
                .Include(c => c.KeyboardDetails)
                .Where(t => t.Name == name);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
