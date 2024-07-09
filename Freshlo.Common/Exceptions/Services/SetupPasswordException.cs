using Freshlo.DomainEntities.Employee;
using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.Common.Exceptions.Services
{
   public class SetupPasswordException : Exception
    {
        public SetupPasswordException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        public SetupPasswordException(int errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        public int ErrorCode { get; set; }
        public Employee info { get; set; }

        public const int ProcessCompleted = 1;
        public const int PasswordNotSetup = 2;
        public const int OtpExpired = 3;
        public const int EmailVerified = 4;
        public const int InvalidOtp = 5;
        public const int Emailconfiguration = 6;
    }
}
