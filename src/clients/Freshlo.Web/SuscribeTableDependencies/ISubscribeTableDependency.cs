using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.SuscribeTableDependencies
{
    public interface ISubscribeTableDependency
    {
      void SubscribeTableDependency(string connectionString);
    }
}
