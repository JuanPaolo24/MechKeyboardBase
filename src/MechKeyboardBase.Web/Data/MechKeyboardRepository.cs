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
        private readonly MechKeyboardBaseDbContext context;

        public MechKeyboardRepository(MechKeyboardBaseDbContext context)
        {
            this.context = context;
        }
        public async Task<Keyboard[]> GetAllKeyboardsAsync(bool includeDetails = false)
        {
            IQueryable<Keyboard> query = context.Keyboard;

            if (includeDetails)
            {
                query = query
                    .Include(c => c.KeyboardDetails);
            }

            return await query.ToArrayAsync();
        }


        public async Task<Keyboard> GetKeyboardByIdAsync(string id, bool includeDetails = false)
        {
            IQueryable<Keyboard> query = context.Keyboard;

            if (includeDetails)
            {
                query = query
                    .Include(c => c.KeyboardDetails);
            }

            query = query
                .Where(t => t.ID == id);

            return await query.FirstOrDefaultAsync();

        }

        public async Task<Keyboard> GetKeyboardByNameAsync(string name, bool includeDetails = false)
        {
            IQueryable<Keyboard> query = context.Keyboard;

            if (includeDetails)
            {
                query = query
                    .Include(c => c.KeyboardDetails);
            }

            query = query
                .Where(t => t.Name == name);

            return await query.FirstOrDefaultAsync();
        } 
    }
}
