using Microsoft.AspNetCore.Mvc;
using oldprogrammer_authentication.services.Register;
using oldprogrammer_authetication.core.Exceptions;
using oldprogrammer_authetication.core.Inputs;

namespace oldprogrammer_authentication.Controllers
{
    [Route("api/register")]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService _registerService;
        private readonly ILogger<RegisterController> _logger;
        public RegisterController(IRegisterService registerService, ILogger<RegisterController> logger)
        {
            _registerService = registerService;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> RegisterNewUser([FromBody] RegisterInput registerInput)
        {
            _logger.LogInformation("System tries to create new user, userInput {RegisterInput}", registerInput);
            try
            {
                bool result = await _registerService.RegisterUser(registerInput);

                if (result)
                {
                    return Created("api/register", 
                    new 
                    {
                        registerInput.Email
                    });
                }
                else
                {
                    return StatusCode(StatusCodes.Status304NotModified, $"Email: {registerInput.Email} was not registered");
                }
            }
            catch (GeneralException ex)
            {
                _logger.LogError(ex, "Controller: RegisterController, Method: RegisterNewUser, CodeReadon: {Code}, Reason: {Reason}", 
                    ex.GeneralReason.Code, ex.GeneralReason.MessageReason);

                return StatusCode(StatusCodes.Status500InternalServerError, "Error when it tried to create new user");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Controller: RegisterController, Method: RegisterNewUser, Exception Message: {Message}", ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, "Error when it tried to create new user");
            }
        }

        [HttpGet("resendConfirmationEmail")]
        public async Task<IActionResult> ResendConfirmationEmail(string email)
        {
            try
            {
                await _registerService.ResendConfirmationEmail(email);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Controller: RegisterController, Method: ResendConfirmationEmail, Exception Message: {Message}", ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, "Error when it tried to resend confirmation email");
            }
        }

        [HttpGet("confirmToken")]
        public async Task<IActionResult> ConfirmToken(string email, string token)
        {
            try
            {
                bool result = await _registerService.ConfirmToken(new ConfirmTokenInput
                {
                    Email = email,
                    Token = token
                });

                if (result)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(StatusCodes.Status304NotModified, $"Token for Email: {email} was not confirmed");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Controller: RegisterController, Method: ConfirmToken, Exception Message: {Message}", ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, "Error when it tried to confirm token");
            }
        }

        [HttpGet("ping")]
        public string Ping()
        {
            return "Register Controller is working";
        }
    }
}
