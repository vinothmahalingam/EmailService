using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailService.Models;

namespace EmailService.Inteface {
    public interface IEmailSender {
        Task SendEmailAsync (Email email);
    }
}