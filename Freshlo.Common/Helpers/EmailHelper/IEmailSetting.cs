using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.Common.Helpers.EmailHelper
{
   public interface IEmailSetting
    {
        string NetworkUserName { get; }
        string NetworkPassword { get; }
        string SmtpHost { get; }
        int SmtpPort { get; }
    }
}
