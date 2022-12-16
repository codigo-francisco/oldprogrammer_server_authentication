using Microsoft.EntityFrameworkCore;
using oldprogrammer_authentication.core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oldprogrammer_authentication.domain.Base
{
    public class RepositoryBase<T, C> : IRepositoryBase<T, C> where T : class where C : DbContext
    {
        protected C Context { get; set; }
        public RepositoryBase(
            C _context
        )
        {
            Context = _context;
        }
        public C GetContext()
        {
            return Context;
        }

        public DbSet<T> GetRepository()
        {
            return Context.Set<T>();
        }

        public IQueryable<T> GetTable()
        {
            return Context.Set<T>().AsQueryable();
        }
    }
}
