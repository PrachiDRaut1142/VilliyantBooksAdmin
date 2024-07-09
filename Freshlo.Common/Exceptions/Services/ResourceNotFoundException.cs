using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.Common.Exceptions.Service
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException()
        {

        }

        public ResourceNotFoundException(string message)
            : base(message)
        {

        }
    }
}
