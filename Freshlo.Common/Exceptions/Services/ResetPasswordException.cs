using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.Common.Exceptions.Service
{
    public class ResetPasswordException: Exception
    {
        public ResetPasswordException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        public ResetPasswordException(int errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        public int ErrorCode { get; set; }

        public const int OtpExpired = 1;
        public const int OtpDoesNotMatch = 2;
        public const int PasswordMatchesPreviousPasswords = 3;
    }
}
