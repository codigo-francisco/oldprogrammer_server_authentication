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
    public class EmailHttpClient : IEmailHttpClient
    {
        private readonly string _emailSendConfirmationEndPoint;
        private readonly ILogger<EmailHttpClient> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public EmailHttpClient(IConfiguration configuration, ILogger<EmailHttpClient> logger, IHttpClientFactory httpClientFactory)
        {
            _emailSendConfirmationEndPoint = configuration.GetRequiredSection("Endpoints").GetRequiredSection("EmailSystem")["SendConfirmationEndPoint"];
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task SendConfirmationEmail(SendConfirmationEmail sendConfirmationEmail)
        {
            try
            {
                _logger.LogInformation("System Email will try to send an confirmation email for {Email}", sendConfirmationEmail.Email);

                using var httpClient = _httpClientFactory.CreateClient();

                var jsonContent = JsonContent.Create(sendConfirmationEmail);

                var responseMessage = await httpClient.PostAsync(_emailSendConfirmationEndPoint, jsonContent);
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
