using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MechKeyboardBase.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace MechKeyboardBase.Web.Data
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
                 .Include(c => c.KeyboardDetails);

            return await query.ToArrayAsync();
        }

        public async Task<Keyboard[]> GetKeyboardByKeyboardDetails(KeyboardBuild keyboardBuild)
        {
            IQueryable<Keyboard> query = _context.Keyboard;

            if (keyboardBuild != null)
            {
                if (!string.IsNullOrEmpty(keyboardBuild.Case))
                    query = query.Where(x => x.KeyboardDetails.Case == keyboardBuild.Case);
                if (!string.IsNullOrEmpty(keyboardBuild.PCB))
                    query = query.Where(x => x.KeyboardDetails.PCB == keyboardBuild.PCB);
                if (!string.IsNullOrEmpty(keyboardBuild.Plate))
                    query = query.Where(x => x.KeyboardDetails.Plate == keyboardBuild.Plate);
                if (!string.IsNullOrEmpty(keyboardBuild.Keycaps))
                    query = query.Where(x => x.KeyboardDetails.Keycaps == keyboardBuild.Keycaps);
                if (!string.IsNullOrEmpty(keyboardBuild.Switch))
                    query = query.Where(x => x.KeyboardDetails.Switch == keyboardBuild.Switch);
            }

            query = query
                .Include(c => c.KeyboardDetails);

            return await query.ToArrayAsync();

        }

        public async Task<Keyboard[]> GetKeyboardByUsernameAsync(string username)
        {
            IQueryable<Keyboard> query = _context.Keyboard;

            query = query
                .Include(c => c.KeyboardDetails)
                .Where(t => t.Username == username);

            return await query.ToArrayAsync();
        }


        public async Task<Keyboard> GetKeyboardByNameAndUsernameAsync(string name, string username)
        {
            IQueryable<Keyboard> query = _context.Keyboard;

            query = query
                .Include(c => c.KeyboardDetails)
                .Where(t => t.Username == username && t.Name == name);


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
