using oldprogrammer_authetication.core.Inputs;
using oldprogrammer_authetication.core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oldprogrammer_authentication.services.Register
{
    public class RegistrationService : IRegisterService
    {
        private readonly IRegistrationRepository _registerRepository;
        public RegistrationService(
            IRegistrationRepository registerRepository
        )
        {
            _registerRepository = registerRepository;
        }
        public Task<bool> RegisterUser(RegisterInput registerInput)
        {
            return _registerRepository.RegisterUser(registerInput);
        }
        public Task<bool> ConfirmToken(ConfirmTokenInput confirmTokenInput)
        {
            return _registerRepository.ConfirmEmailRegistration(confirmTokenInput);
        }

        public Task ResendConfirmationEmail(string email)
        {
            return _registerRepository.ResendConfirmationEmail(email);
        }
    }
}
