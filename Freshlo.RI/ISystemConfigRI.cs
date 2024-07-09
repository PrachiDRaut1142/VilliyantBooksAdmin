using Freshlo.DomainEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.RI
{
   public interface ISystemConfigRI
    {
        SecurityConfig GetSecurityConfig();
        Emailconfig GetEmailConfig();

    }
}
