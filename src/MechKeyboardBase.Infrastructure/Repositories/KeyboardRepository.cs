using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MechKeyboardBase.Core.Entities;
using MechKeyboardBase.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;

namespace MechKeyboardBase.Infrastructure.Repositories
{
    public class KeyboardRepository : IKeyboardRepository
    {
        private readonly MechKeyboardBaseDbContext _context;

        public KeyboardRepository(MechKeyboardBaseDbContext context)
        {
            _context = context;
        }

        public void Add(Keyboard keyboard)
        {
            if (keyboard == null)
            {
                throw new ArgumentNullException(nameof(keyboard));
            }

            _context.Add(keyboard);
        }

        public void Delete(Keyboard keyboard)
        {
            if (keyboard == null)
            {
                throw new ArgumentNullException(nameof(keyboard));
            }

            _context.Remove(keyboard);
        }

        public async Task<IEnumerable<Keyboard>> GetAllKeyboardAsync()
        {
            return await _context.Keyboard
                .OrderByDescending(x => x.KeyboardId)
                .ToListAsync();
        }


        public async Task<IEnumerable<Keyboard>> GetKeyboardsByPageAsync(int pageNumber, int pageSize)
        {

            IQueryable<Keyboard> query = _context.Keyboard;

            query = query.OrderByDescending(x => x.KeyboardId);

            var paginatedList = await Paginator<Keyboard>.CreateAsync(query, pageNumber, pageSize);

            return paginatedList.ToArray();
        }

        public async Task<Keyboard> GetKeyboardByNameAndUsernameAsync(string keyboardname, string username)
        {
            return await _context.Keyboard
                .Where(t => t.Username == username && t.KeyboardName == keyboardname)
                .FirstOrDefaultAsync();
        }

        public async Task<Keyboard> GetKeyboardByName(string keyboardname)
        {
            return await _context.Keyboard
                .Where(t => t.KeyboardName == keyboardname)
                .FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<Keyboard>> GetKeyboardByUsernameAsync(string username)
        {
            return await _context.Keyboard
                .Where(t => t.Username == username)
                .OrderByDescending(x => x.KeyboardId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Keyboard>> GetKeyboardPageByUsernameAsync(int pageNumber, int pageSize, string username)
        {
            IQueryable<Keyboard> query = _context.Keyboard;

            query = query
                .Where(t => t.Username == username)
                .OrderByDescending(x => x.KeyboardId);

            var paginatedList = await Paginator<Keyboard>.CreateAsync(query, pageNumber, pageSize);

            return paginatedList.ToArray();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        
    }
}
