using oldprogrammer_authetication.core.Inputs;
using oldprogrammer_authetication.core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oldprogrammer_authentication.services.Register
{
    public class RegisterService : IRegisterService
    {
        private readonly IRegisterRepository _registerRepository;
        public RegisterService(
            IRegisterRepository registerRepository
        )
        {
            _registerRepository = registerRepository;
        }
        public Task<bool> RegisterUser(RegisterInput registerInput)
        {
            return _registerRepository.RegisterUser(registerInput);
        }
    }
}
