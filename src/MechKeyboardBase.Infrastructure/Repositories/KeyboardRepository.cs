using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MechKeyboardBase.Core.Entities;
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

        public async Task<Keyboard[]> GetAllKeyboardsAsync()
        {
            IQueryable<Keyboard> query = _context.Keyboard;

            query = query
                 .Include(c => c.Details);

            return await query.ToArrayAsync();
        }

        public async Task<Keyboard[]> GetKeyboardByKeyboardDetails(KeyboardDetails keyboardDetails)
        {
            IQueryable<Keyboard> query = _context.Keyboard;

            if (keyboardDetails != null)
            {
                if (!string.IsNullOrEmpty(keyboardDetails.Case))
                    query = query.Where(x => x.Details.Case == keyboardDetails.Case);
                if (!string.IsNullOrEmpty(keyboardDetails.PCB))
                    query = query.Where(x => x.Details.PCB == keyboardDetails.PCB);
                if (!string.IsNullOrEmpty(keyboardDetails.Plate))
                    query = query.Where(x => x.Details.Plate == keyboardDetails.Plate);
                if (!string.IsNullOrEmpty(keyboardDetails.Keycaps))
                    query = query.Where(x => x.Details.Keycaps == keyboardDetails.Keycaps);
                if (!string.IsNullOrEmpty(keyboardDetails.Switch))
                    query = query.Where(x => x.Details.Switch == keyboardDetails.Switch);
            }

            query = query
                .Include(c => c.Details);

            return await query.ToArrayAsync();

        }

        public async Task<Keyboard[]> GetKeyboardByUsernameAsync(string username)
        {
            IQueryable<Keyboard> query = _context.Keyboard;

            query = query
                .Include(c => c.Details)
                .Where(t => t.Username == username);

            return await query.ToArrayAsync();
        }


        public async Task<Keyboard> GetKeyboardByNameAndUsernameAsync(string keyboardname, string username)
        {
            IQueryable<Keyboard> query = _context.Keyboard;

            query = query
                .Include(c => c.Details)
                .Where(t => t.Username == username && t.KeyboardName == keyboardname);


            return await query.FirstOrDefaultAsync();
        }


        public async Task<Keyboard> GetKeyboardByNameAsync(string keyboardname)
        {
            IQueryable<Keyboard> query = _context.Keyboard;

            query = query
                .Include(c => c.Details)
                .Where(t => t.KeyboardName == keyboardname);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
