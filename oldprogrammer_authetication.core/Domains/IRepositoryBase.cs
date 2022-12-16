using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using oldprogrammer_authetication.core.Domains;

namespace oldprogrammer_authentication.core.Domains
{
    public interface IRepositoryBase<T, C> : IRepository<T> where T : class where C : DbContext
    {
        C GetContext();
    }
}
