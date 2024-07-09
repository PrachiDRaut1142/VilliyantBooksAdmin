using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.RI
{
    public interface IDbConfig
    {
        string ConnectionString { get; }
        string BusinessInfo { get; }

    }
}