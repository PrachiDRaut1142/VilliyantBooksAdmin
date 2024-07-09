using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.Common.Helpers.EmailHelper
{
   public class Email
    {
        public Email()
        {

        }
        public Email(string recipient, string sender, string subject, string body, bool isBodyHtml = false)
        {
            Body = body;
            IsBodyHtml = isBodyHtml;
            Recipient = recipient;
            Subject = subject;
        }

        public string Sender { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
    }
}

