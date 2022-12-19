using oldprogrammer_authetication.core.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oldprogrammer_authentication.services.Register
{
    public interface IRegisterService
    {
        Task<bool> RegisterUser(RegisterInput registerInput);
        Task<bool> ConfirmToken(ConfirmTokenInput confirmTokenInput);
        Task ResendConfirmationEmail(string email);
    }
}
