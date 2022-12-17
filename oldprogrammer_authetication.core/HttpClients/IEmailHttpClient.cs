using oldprogrammer_authetication.core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oldprogrammer_authetication.core.HttpClients
{
    public interface IEmailHttpClient
    {
        Task SendConfirmationEmail(SendConfirmationEmail sendConfirmationEmail);
    }
}
