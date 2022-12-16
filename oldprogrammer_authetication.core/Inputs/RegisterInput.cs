using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oldprogrammer_authetication.core.Inputs
{
    public class RegisterInput
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"Email: {Email}, Password: {Password}";
        }
    }
}
