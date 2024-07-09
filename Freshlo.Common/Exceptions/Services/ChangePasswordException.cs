using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.Common.Exceptions.Service
{
    public class ChangePasswordException
        : Exception
    {
        public ChangePasswordException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        public ChangePasswordException(int errorCode, string message)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        public int ErrorCode { get; set; }

        public const int IncorrectPassword = 1;
        public const int PasswordMatchesPreviousPasswords = 2;
    }
}
