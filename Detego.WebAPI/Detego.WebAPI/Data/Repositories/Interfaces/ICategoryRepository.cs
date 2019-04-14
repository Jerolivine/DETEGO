using Detego.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Detego.WebAPI.Data.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();
    }
}
