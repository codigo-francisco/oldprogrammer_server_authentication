using Microsoft.AspNetCore.Identity;
using oldprogrammer_authetication.core.Models;

namespace oldprogrammer_authentication.Security
{
    public class TokenProvider : IUserTwoFactorTokenProvider<AuthenticationUser>
    {
        private string _tokenSecretEmail;
        private readonly IEncryptionString _encryptionString;
        public TokenProvider(IConfiguration configuration, IEncryptionString encryptionString)
        {
            _tokenSecretEmail = configuration["TokenSecretEmail"];
            _encryptionString = encryptionString;
        }
        public Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<AuthenticationUser> manager, AuthenticationUser user)
        {
            return Task.FromResult(manager != null && user != null);
        }

        public Task<string> GenerateAsync(string purpose, UserManager<AuthenticationUser> manager, AuthenticationUser user)
        {
            string value = $"{user.Id}{user.Email}{DateTime.Now.ToLongDateString()}";
            string token = _encryptionString.Encrypt(value, _tokenSecretEmail);
            return Task.FromResult(token);
        }

        public async Task<bool> ValidateAsync(string purpose, string token, UserManager<AuthenticationUser> manager, AuthenticationUser user)
        {
            var resultConfirmation = await manager.ConfirmEmailAsync(user, token);
            return resultConfirmation.Succeeded;
        }
    }
}
