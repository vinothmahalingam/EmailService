using System.Threading.Tasks;
using EmailService.Inteface;
using EmailService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmailService.Controllers {
    [ApiController]
    [Route ("api/[controller]")]

    public class EmailController : ControllerBase {
        private readonly IEmailSender _emailSender;
        private readonly ILogger<EmailController> _logger;

        public EmailController (IEmailSender emailSender, ILogger<EmailController> logger) {
            _emailSender = emailSender;
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route ("SendEmail")]
        public async Task<IActionResult> SendEmail ([FromForm] Email email) {
            ApiResponse response = new ApiResponse ();
            try {
                await _emailSender.SendEmailAsync (email);
                response.Message = "Email Sent successfully.";
                response.Status = true;
                return Ok (response);
            } catch (System.Exception ex) {
                response.Message = "Error has been occured in Email Sending. " + ex.Message;
                response.Status = true;
                return Ok (response);
                throw;
            }

        }
    }
}