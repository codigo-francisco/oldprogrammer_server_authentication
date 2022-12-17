using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oldprogrammer_authetication.core.DTO
{
    public class SendConfirmationEmail
    {
        public SendConfirmationEmail(string email, string token) 
        {
            Email = email;
            Token = token;
        }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
