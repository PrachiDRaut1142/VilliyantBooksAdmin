﻿using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoDecodeURLParameters.Security
{
    public class CustomIDataProtection
    {
        private readonly IDataProtector protector;

        public CustomIDataProtection(IDataProtectionProvider dataProtectionProvider,
                              UniqueCode uniqueCode)
        {
            protector = dataProtectionProvider.CreateProtector(
               uniqueCode.BankIdRouteValue);
        }

        public string Decode(string data)
        {
            return protector.Protect(data);
        }

        public string Encode(string data)
        {
            return protector.Unprotect(data);
        }
    }
}

