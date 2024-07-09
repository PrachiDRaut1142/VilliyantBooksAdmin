using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.Common.Exceptions.Service
{
    public class LoginException
        : Exception
    {
        public LoginException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        public LoginException(int errorCode, string message)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        
        public const int ExceededLoginAttempts = 1;
        public const int InvalidPassword = 2;

        public int ErrorCode { get; set; }
    }
}
