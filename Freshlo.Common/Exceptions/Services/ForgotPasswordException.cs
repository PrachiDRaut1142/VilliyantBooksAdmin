using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.Common.Exceptions.Service
{
    public class ForgotPasswordException : Exception
    {
        public ForgotPasswordException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        public ForgotPasswordException(int errorCode, string message)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        public const int EmailAddressNotRegistered = 1;
        public const int AccountDeactivated = 2;
        public const int AccountNotVerified = 3;

        public int ErrorCode { get; set; }
    }
}
