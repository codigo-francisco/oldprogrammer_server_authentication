using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace oldprogrammer_authentication.domain.Base
{
    public interface IRepositoryBase<T, C> where T : class where C : DbContext
    {
        DbSet<T> GetRepository();
        C GetContext();
        IQueryable<T> GetTable();
    }
}
