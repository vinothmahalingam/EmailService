using System;
using System.IO;
using System.Threading.Tasks;
using EmailService.Inteface;
using EmailService.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace EmailService.Service {
    public class EmailSender : IEmailSender {
        public async Task SendEmailAsync (Email email) {
            try {

                string[] ToEmailIdsList = email.ToEmailIds.Split (';');

                var mimeMessage = new MimeMessage ();
                mimeMessage.From.Add (new MailboxAddress (email.FromAddressTitle,
                    email.SmptUserName
                ));

                foreach (var toMailAddress in ToEmailIdsList) {
                    MailboxAddress toEmail = new MailboxAddress ("",
                        toMailAddress);
                    mimeMessage.To.Add (toEmail);
                }

                mimeMessage.Subject = email.Subject; //Subject
                BodyBuilder bodyBuilder = new BodyBuilder ();
                if ((email.ContentType).ToLower () == "plain-text") {
                    mimeMessage.Body = new TextPart ("plain") {
                    Text = email.Message
                    };
                } else if ((email.ContentType).ToLower () == "html") {
                    bodyBuilder.HtmlBody = email.Message;
                }
                if (email.Attachments != null) {
                    foreach (var file in email.Attachments) {
                        if (file.Length > 0) {
                            using (var memorystream = new MemoryStream ()) {
                                file.CopyTo (memorystream);
                                var fileBytes = memorystream.ToArray ();
                                bodyBuilder.Attachments.Add (file.FileName, fileBytes, ContentType.Parse (file.ContentType));
                            }
                        }
                    }
                }
                mimeMessage.Body = bodyBuilder.ToMessageBody ();
                
                using (var client = new SmtpClient ()) {
                    client.Connect (email.SmptServer, email.Port, true);
                    client.Authenticate (
                        email.SmptUserName,
                        email.SmptPassword
                    );
                    await client.SendAsync (mimeMessage);
                    await client.DisconnectAsync (true);
                }
            } catch (Exception ex) {
                throw ex;
            }
        }

        public Task SendSmsAsync (string number, string message) {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult (0);
        }
    }
}