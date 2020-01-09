using System;
using Microsoft.AspNetCore.Http;

namespace EmailService.Models {
    public class Email {

        public string FromAddressTitle { get; set; }   
        public string ToEmailIds { get; set; }        
        public string Subject { get; set; }
        public string ContentType {get;set;}
        public string Message { get; set; }
        public string SmptServer { get; set; }        
        public Int32 Port {get;set;}
        public IFormFileCollection Attachments {get;set;} 
        public string SmptUserName { get; set; }
        public string SmptPassword { get; set; }

    }
}