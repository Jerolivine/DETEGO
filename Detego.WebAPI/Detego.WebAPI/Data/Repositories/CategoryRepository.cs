using Detego.WebAPI.Data.Repositories.Interfaces;
using Detego.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Detego.WebAPI.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {

        private DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _context.Category.ToListAsync();
        }
    }
}
