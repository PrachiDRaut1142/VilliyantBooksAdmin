using Freshlo.DomainEntities;
using Freshlo.Repository;
using Freshlo.RI;
using Freshlo.SI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.Services
{
   public class SystemConfigServices:ISystemConfigSI
    {
        private ISystemConfigRI _systemConfigRepository { get; }
        public SystemConfigServices(ISystemConfigRI systemConfigRepository)
        {
            _systemConfigRepository = systemConfigRepository;
        }
        public Task<SecurityConfig> GetSecurityConfigAsync()
        {
            return Task.Run(() =>
            {
                return _systemConfigRepository.GetSecurityConfig();
            });
        }
    }
}
