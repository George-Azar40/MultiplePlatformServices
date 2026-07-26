using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplePlatformServices.BLL.Services.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(String email , String subject , String message);
    }
}
