using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.Common.Helpers.EmailHelper
{
   public class EmailSetting : IEmailSetting
    {
        public EmailSetting(string networkUserName, string networkPassword, string smtpHost, int smtpPort)
        {
            NetworkUserName = networkUserName;
            NetworkPassword = networkPassword;
            SmtpHost = smtpHost;
            SmtpPort = smtpPort;
        }

        public string NetworkUserName { get; }
        public string NetworkPassword { get; }
        public string SmtpHost { get; }
        public int SmtpPort { get; }
    }
}
