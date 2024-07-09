using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Helpers
{
    public class FinanceHelper
    {
        private static Dictionary<int, string> _StatusColor;
        public static Dictionary<int, string> StatusColor
        {
            get
            {
                if (_StatusColor == null)
                {
                    _StatusColor = new Dictionary<int, string>
                    {
                        [1] = "success",
                        [2] = "danger",
                        [3] = "warning"
                    };
                }
                return _StatusColor;
            }
            set
            {
                _StatusColor = value;
            }
        }
        private static List<SelectListItem> _inwardList;
        public static List<SelectListItem> InwardList
        {
            get
            {
                if (_inwardList == null)
                {
                    _inwardList = new List<SelectListItem>
                    {
                        new SelectListItem { Value = "1", Text = "Swiggy" },
                        new SelectListItem { Value = "2", Text = "Zomato" },
                        new SelectListItem { Value = "3", Text = "Online Payment Settlement" },
                        new SelectListItem { Value = "4", Text = "Cash Withdrawal" },
                        new SelectListItem { Value = "5", Text = "Loan" },
                        new SelectListItem { Value = "6", Text = "Capital Investment" },
                        new SelectListItem { Value = "7", Text = "Others" },
                       
                    };
                }
                return _inwardList;
            }

            set
            {
                _inwardList = value;
            }
        }
        private static List<SelectListItem> _outwardList;
        public static List<SelectListItem> OutwardList
        {
            get
            {
                if (_outwardList == null)
                {
                    _outwardList = new List<SelectListItem>
                    {
                      
                        new SelectListItem { Value = "1", Text = "Payment towards PO" },
                        new SelectListItem { Value = "2", Text = "Adhoc Purchase" },
                        new SelectListItem { Value = "3", Text = "Salary" },
                        new SelectListItem { Value = "4", Text = "Electricity Bill" },
                        new SelectListItem { Value = "5", Text = "Phone Bill" },
                        new SelectListItem { Value = "6", Text = "Internet Bill " },
                        new SelectListItem { Value = "7", Text = "Marketing" },
                        new SelectListItem { Value = "8", Text = "Stationary" },
                        new SelectListItem { Value = "9", Text = "Rent" },
                        new SelectListItem { Value = "11", Text = "Other Items Purchase" },
                    };
                }
                return _outwardList;
            }

            set
            {
                _outwardList = value;
            }
        }
    }
}
