using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Helpers
{
    public class PricelistHelper
    {
        private static List<SelectListItem> _statusList;
        public static List<SelectListItem> StatusList
        {
            get
            {
                if (_statusList == null)
                {
                    _statusList = new List<SelectListItem>
                    {
                        new SelectListItem { Value = "1", Text = "Yes" },
                        new SelectListItem { Value = "0", Text = "No" },

                    };
                }
                return _statusList;
            }

            set
            {
                _statusList = value;
            }
        }
    }
}
