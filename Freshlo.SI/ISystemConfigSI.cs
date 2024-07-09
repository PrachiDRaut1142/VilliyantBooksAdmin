using Freshlo.DomainEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.SI
{
    public interface ISystemConfigSI
    {
        Task<SecurityConfig> GetSecurityConfigAsync();

    }
}
