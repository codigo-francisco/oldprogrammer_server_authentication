using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oldprogrammer_authetication.core.Domains
{
    public interface IRepository<T> where T : class
    {
        DbSet<T> GetRepository();
        IQueryable<T> GetTable();
    }
}
