using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using oldprogrammer_authetication.core.Exceptions;
using oldprogrammer_authetication.core.Inputs;
using oldprogrammer_authetication.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oldprogrammer_authetication.core.Repositories
{
    public class RegisterRepository : IRegisterRepository
    {
        private readonly UserManager<AuthenticationUser> _userManager;
        private readonly ILogger<RegisterRepository> _logger;

        public RegisterRepository(
            UserManager<AuthenticationUser> userManager,
            ILogger<RegisterRepository> logger
        ) 
        {
            _logger = logger;
            _userManager = userManager;
        }
        public async Task<bool> RegisterUser(RegisterInput registerInput)
        {
            bool result = false;
            var userDB = await _userManager.FindByEmailAsync(registerInput.Email);

            if (userDB == null)
            {
                var newUser = new AuthenticationUser
                {
                    Email = registerInput.Email
                };

                var identityResult = await _userManager.CreateAsync(newUser, registerInput.Password);

                if (identityResult.Errors.Any())
                {
                    foreach (var error in identityResult.Errors)
                    {
                        _logger.LogError("Error: Code: {Code}, Description: {Description}", error.Code, error.Description);
                    }

                    throw new Exception("Errors detected when system tried to register new user, method: RegisterUser");
                }

                result = identityResult.Succeeded;
            }
            else
            {
                throw new GeneralExceptionEmailExists();
            }

            return result;
        }
    }
}
