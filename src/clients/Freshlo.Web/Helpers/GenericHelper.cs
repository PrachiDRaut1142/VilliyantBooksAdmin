using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Helpers
{
    public class GenericHelper
    {
        // Security setting
        private static List<SelectListItem> _PasswordDays;
        public static List<SelectListItem> PasswordDays
        {
            get
            {
                if (_PasswordDays == null)
                {
                    _PasswordDays = new List<SelectListItem>
                    {
                        new SelectListItem { Value = "7", Text = "7 days" },
                        new SelectListItem { Value = "30", Text = "30 days" },
                        new SelectListItem { Value = "60", Text = "60 days" },
                        new SelectListItem { Value = "90", Text = "90 days" },
                        new SelectListItem { Value = "180", Text = "180 days" },
                        new SelectListItem { Value = "365", Text = "365 days" },

                    };
                }
                return _PasswordDays;
            }

            set
            {
                _PasswordDays = value;
            }
        }
    }
}
