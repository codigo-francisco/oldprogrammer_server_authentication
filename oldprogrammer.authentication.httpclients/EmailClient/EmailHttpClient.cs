using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using oldprogrammer_authetication.core.DTO;
using oldprogrammer_authetication.core.HttpClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace oldprogrammer.authentication.httpclients.EmailClient
{
    public class EmailHttpClient : HttpClient, IEmailHttpClient
    {
        private readonly string _emailSendConfirmationEndPoint;
        private ILogger<EmailHttpClient> _logger;
        public EmailHttpClient(IConfiguration configuration, ILogger<EmailHttpClient> logger)
        {
            _emailSendConfirmationEndPoint = configuration.GetRequiredSection("Endpoints").GetRequiredSection("SendConfirmationEndPoint").Value;
            _logger = logger;
        }

        public async Task SendConfirmationEmail(SendConfirmationEmail sendConfirmationEmail)
        {
            try
            {
                _logger.LogInformation("System Email will try to send an confirmation email for {Email}", sendConfirmationEmail.Email);

                var jsonContent = JsonContent.Create(sendConfirmationEmail);

                var responseMessage = await PostAsync(_emailSendConfirmationEndPoint, jsonContent);
                bool resultMessage = responseMessage.IsSuccessStatusCode;

                if (resultMessage)
                {
                    _logger.LogInformation("Confirmation email was succesful sent for {Email}", sendConfirmationEmail.Email);
                }
                else
                {
                    string? reasonPhrase = responseMessage.ReasonPhrase;
                    int errorCode = (int)responseMessage.StatusCode;
                    _logger.LogError("Confirmation email was succesful sent for {Email}, StatusCode: {ErrorCode}, Reason: {ReasonPhrase}",
                        sendConfirmationEmail.Email, errorCode, reasonPhrase);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "A internal server error ocurred when server tried to send a confirmation email for {Email}, MessageError: {MessageError}",
                    sendConfirmationEmail.Email, ex.Message);
            }
            
        }
    }
}
