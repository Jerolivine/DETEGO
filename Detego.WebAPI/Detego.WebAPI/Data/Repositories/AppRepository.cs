using Detego.WebAPI.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Detego.WebAPI.Data.Repositories
{
    public class AppRepository : IAppRepository
    {
        public DataContext _context;

        public AppRepository(DataContext context)
        {
            this._context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
