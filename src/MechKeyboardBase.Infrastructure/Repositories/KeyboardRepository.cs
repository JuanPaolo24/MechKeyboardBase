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

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Keyboard[]> GetAllKeyboardAsync()
        {

            IQueryable<Keyboard> query = _context.Keyboard;

            return await query.ToArrayAsync();
        }


        public async Task<Keyboard[]> GetKeyboardsByPageAsync(int pageNumber, int pageSize)
        {
            IQueryable<Keyboard> query = _context.Keyboard;

            var paginatedList = await Paginator<Keyboard>.CreateAsync(query, pageNumber, pageSize);

            return paginatedList.ToArray();
        }

        public async Task<Keyboard[]> FilterKeyboardsAsync(Keyboard keyboard)
        {
            IQueryable<Keyboard> query = _context.Keyboard;

            if (!string.IsNullOrEmpty(keyboard.KeyboardName))
                query = query.Where(x => x.KeyboardName == keyboard.KeyboardName);
                if (!string.IsNullOrEmpty(keyboard.Case))
                    query = query.Where(x => x.Case == keyboard.Case);
                if (!string.IsNullOrEmpty(keyboard.PCB))
                    query = query.Where(x => x.PCB == keyboard.PCB);
                if (!string.IsNullOrEmpty(keyboard.Plate))
                    query = query.Where(x => x.Plate == keyboard.Plate);
                if (!string.IsNullOrEmpty(keyboard.Keycaps))
                    query = query.Where(x => x.Keycaps == keyboard.Keycaps);
                if (!string.IsNullOrEmpty(keyboard.Switch))
                    query = query.Where(x => x.Switch == keyboard.Switch);



            return await query.ToArrayAsync();

        }


        public async Task<Keyboard> GetKeyboardByNameAndUsernameAsync(string keyboardname, string username)
        {
            IQueryable<Keyboard> query = _context.Keyboard;

            query = query
                .Where(t => t.Username == username && t.KeyboardName == keyboardname);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Keyboard[]> GetKeyboardByUsernameAsync(string username)
        {
            IQueryable<Keyboard> query = _context.Keyboard;

            query = query
                .Where(t => t.Username == username);

            return await query.ToArrayAsync();
        }
    }
}
