using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using oldprogrammer_authetication.core.DTO;
using oldprogrammer_authetication.core.Exceptions;
using oldprogrammer_authetication.core.HttpClients;
using oldprogrammer_authetication.core.Inputs;
using oldprogrammer_authetication.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace oldprogrammer_authetication.core.Repositories
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly UserManager<AuthenticationUser> _userManager;
        private readonly ILogger<RegistrationRepository> _logger;
        private readonly IEmailHttpClient _emailSystem;
        public RegistrationRepository(
            UserManager<AuthenticationUser> userManager,
            ILogger<RegistrationRepository> logger,
            IEmailHttpClient emailSystem
        ) 
        {
            _logger = logger;
            _userManager = userManager;
            _emailSystem = emailSystem;
        }

        public async Task<bool> ConfirmEmailRegistration(ConfirmTokenInput confirmTokenInput)
        {
            try
            {
                _logger.LogInformation("System tries to confirm token for email: {Email}", confirmTokenInput.Email);
                var userDB = await _userManager.FindByEmailAsync(confirmTokenInput.Email);

                if (userDB != null)
                {
                    var confirmEmailResult = await _userManager.ConfirmEmailAsync(userDB, confirmTokenInput.Token);

                    return confirmEmailResult.Succeeded;
                }
                else
                {
                    throw new GeneralException("UserNotFound");
                }
            }
            catch (GeneralException ex)
            {
                _logger.LogError(ex, "An error ocurred when it tries to confirm token for email:{Email} method: ConfirmEmailRegistration, class: RegistrationRepository, MessageException: {Message}", 
                    confirmTokenInput.Email, ex.Message);
                throw;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error ocurred when it tries to confirm token for email:{Email} method: ConfirmEmailRegistration, class: RegistrationRepository, MessageException: {Message}",
                    confirmTokenInput.Email, ex.Message);
                throw;
            }
        }

        public async Task<bool> RegisterUser(RegisterInput registerInput)
        {
            bool result = false;
            var userDB = await _userManager.FindByEmailAsync(registerInput.Email);

            if (userDB == null)
            {
                var newUser = new AuthenticationUser
                {
                    Email = registerInput.Email,
                    UserName = registerInput.Email
                };

                var identityResult = await _userManager.CreateAsync(newUser, registerInput.Password);

                if (identityResult.Errors.Any())
                {
                    foreach (var error in identityResult.Errors)
                    {
                        _logger.LogError("Error: Code: {Code}, Description: {Description}", error.Code, error.Description);
                    }

                    throw new GeneralException(identityResult.Errors.First().Code);
                }

                result = identityResult.Succeeded;

                if (result)
                {
                    var tokenEmail = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                    var sendConfirmationEmail = new SendConfirmationEmail(registerInput.Email, tokenEmail);
                    await _emailSystem.SendConfirmationEmail(sendConfirmationEmail);
                }
            }
            else
            {
                throw new GeneralExceptionEmailExists();
            }

            return result;
        }

        public async Task ResendConfirmationEmail(string email)
        {
            try
            {
                _logger.LogInformation("It tries to resend confirmation email for: {Email}", email);

                var userDB = await _userManager.FindByEmailAsync(email);

                if (userDB != null)
                {
                    var newToken = await _userManager.GenerateEmailConfirmationTokenAsync(userDB);

                    SendConfirmationEmail sendConfirmationEmail = new(email, newToken);

                    await _emailSystem.SendConfirmationEmail(sendConfirmationEmail);
                }
                else
                {
                    throw new GeneralException("UserNotFound");
                }
            }
            catch(GeneralException ex)
            {
                _logger.LogError(ex, "An error ocurred when it tries to resend confirmation email for email:{Email} method: ConfirmEmailRegistration, class: RegistrationRepository, MessageException: {Message}",
                    email, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error ocurred when it tries to resend confirmation email for email:{Email} method: ConfirmEmailRegistration, class: RegistrationRepository, MessageException: {Message}",
                    email, ex.Message);
                throw;
            }
        }
    }
}
