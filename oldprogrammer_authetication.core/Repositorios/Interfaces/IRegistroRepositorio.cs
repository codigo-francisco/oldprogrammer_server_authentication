using oldprogrammer_authetication.core.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oldprogrammer_authetication.core.Repositorios.Interfaces
{
    public interface IRegistroRepositorio
    {
        bool RegistrarUsuario(RegistroInput registroInput);
    }
}
