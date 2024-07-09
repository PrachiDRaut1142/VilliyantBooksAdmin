using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Freshlo.Common.Helpers.EmailHelper
{
   public static class EmailExtension
    {
        public static void Send(this Email email, IEmailSetting emailSettings)
        {
            using (MailMessage mail = new MailMessage(email.Sender, email.Recipient))
            {
                mail.Subject = email.Subject;
                mail.IsBodyHtml = email.IsBodyHtml;
                mail.Body = email.Body;
                using (SmtpClient smtp = new SmtpClient(emailSettings.SmtpHost, emailSettings.SmtpPort))
                {
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(emailSettings.NetworkUserName, emailSettings.NetworkPassword);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(mail);
                }

                //mail.Subject = email.Subject;
                //mail.IsBodyHtml = email.IsBodyHtml;
                //mail.Body = email.Body;
                //using (SmtpClient smtp = new SmtpClient(emailSettings.SmtpHost, emailSettings.SmtpPort))
                //{
                //    smtp.EnableSsl = true;
                //    smtp.Credentials = new NetworkCredential(emailSettings.NetworkUserName, emailSettings.NetworkPassword);
                //    smtp.Send(mail);
                //}
            }
        }
    }
}
