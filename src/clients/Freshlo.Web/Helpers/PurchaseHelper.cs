using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Helpers
{
    public class PurchaseHelper
    {
        private static List<SelectListItem> _procrementList;
        public static List<SelectListItem> ProcurementList
        {
            get
            {
                if (_procrementList == null)
                {
                    _procrementList = new List<SelectListItem>
                    {
                        new SelectListItem { Value = "Freshlo", Text = "Freshlo" },
                        new SelectListItem { Value = "Swiggy", Text = "Swiggy" },
                        new SelectListItem { Value = "Zomato", Text = "Zomato" },
                        new SelectListItem { Value = "All", Text = "All" }
                    };
                }
                return _procrementList;
            }

            set
            {
                _procrementList = value;
            }
        }
        private static Dictionary<string, string> _OrderStatusTag;
        public static Dictionary<string, string> OrderStatusTag
        {
            get
            {
                if (_OrderStatusTag == null)
                {
                    _OrderStatusTag = new Dictionary<string, string>
                    {
                        ["Pending"] = "danger",
                        ["Approved"] = "success",
                        ["Unapproved"] = "warning",
                        ["Ordered"] = "info",
                        ["Procured"] = "success",
                        ["Cancelled"] = "warning",
                        ["Delivered"] = "success"


                    };
                }
                return _OrderStatusTag;
            }

            set
            {
                _OrderStatusTag = value;
            }
        }
        private static List<SelectListItem> _statusList;
        public static List<SelectListItem> StatusList
        {
            get
            {
                if (_statusList == null)
                {
                    _statusList = new List<SelectListItem>
                    {
                        new SelectListItem { Value = "Pending", Text = "Pending" },
                        new SelectListItem { Value = "Approved", Text = "Approved" },
                        new SelectListItem { Value = "UnApproved", Text = "UnApproved" },
                        new SelectListItem { Value = "Procured", Text = "Procured" },
                        new SelectListItem { Value = "Received", Text = "Received" },
                        new SelectListItem { Value = "Cancelled", Text = "Cancelled" },
                        new SelectListItem { Value = "Delieverd", Text = "Delieverd" }
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
