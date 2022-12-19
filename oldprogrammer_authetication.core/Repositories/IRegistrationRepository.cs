using oldprogrammer_authetication.core.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oldprogrammer_authetication.core.Repositories
{
    public interface IRegistrationRepository
    {
        Task<bool> RegisterUser(RegisterInput registerInput);
        Task<bool> ConfirmEmailRegistration(ConfirmTokenInput confirmTokenInput);
        Task ResendConfirmationEmail(string email);
    }
}
