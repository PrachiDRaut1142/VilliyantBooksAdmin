using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Helpers
{
    public class SalesHelpers
    {
        private static Dictionary<string, string> _PaymentStatus;
        public static Dictionary<string,string> PaymentStatus
        {
            get
            {
                if (_PaymentStatus == null)
                {
                    _PaymentStatus = new Dictionary<string, string>
                    {
                        ["Paid"] = "Paid",
                        ["Pending"] = "Pending",
                        ["Partially"] = "Partially",
                        ["NA"] = "NA"
                    };                   
                }
                return _PaymentStatus;
            }

            set
            {
                _PaymentStatus = value;
            }
        }

        private static Dictionary<string, string> _PaymentStatusTag;
        public static Dictionary<string, string> PaymentStatusTag
        {
            get
            {
                if (_PaymentStatusTag == null)
                {
                    _PaymentStatusTag = new Dictionary<string, string>
                    {
                        ["Paid"] = "success",
                        ["Pending"] = "danger",
                        ["Partially"] = "warning",
                        ["NA"] = "warning"
                    };
                }
                return _PaymentStatusTag;
            }

            set
            {
                _PaymentStatusTag = value;
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
                          new SelectListItem { Value = "Ordered", Text = "Ordered" },
                          new SelectListItem { Value = "Confirmed", Text = "Confirmed" },
                        new SelectListItem { Value = "Shipped", Text = "Shipped" },
                        new SelectListItem { Value = "Out for delivery", Text = "Out for delivery" },
                        new SelectListItem { Value = "Refused", Text = "Refused" },
                          new SelectListItem { Value = "Delivered", Text = "Delivered" },
                        new SelectListItem { Value = "Cancelled", Text = "Cancelled" },
                        //new SelectListItem { Value = "Packed", Text = "Packed" },
                        //new SelectListItem { Value = "Dispatched", Text = "Dispatched" },
                        // new SelectListItem { Value = "ReAttempt", Text = "ReAttempt" },
                        // new SelectListItem { Value = "UnDelivered", Text = "UnDelivered" },
                        
                        // new SelectListItem { Value = "Redispatched #2", Text = "Redispatched #2" }
                    };
                }
                return _statusList;
            }

            set
            {
                _statusList = value;
            }
        }

        private static List<SelectListItem> _DeliverySourceList;
        public static List<SelectListItem> DeliverySourceList
        {
            get
            {
                if (_DeliverySourceList == null)
                {
                    _DeliverySourceList = new List<SelectListItem>
                    {
                        new SelectListItem { Value = "Web", Text = "Web" },
                        new SelectListItem { Value = "App", Text = "App" }
                    };
                }
                return _DeliverySourceList;
            }

            set
            {
                _DeliverySourceList = value;
            }
        }

        private static List<SelectListItem> _PaymentStatusList;
        public static List<SelectListItem> PaymentStatusList
        {
            get
            {
                if (_PaymentStatusList == null)
                {
                    _PaymentStatusList = new List<SelectListItem>
                    {
                        new SelectListItem { Value = "Pending", Text = "Pending" },
                        new SelectListItem { Value = "Paid", Text = "Paid" },
                        new SelectListItem { Value = "Partially", Text = "Partially" }

                    };
                }
                return _PaymentStatusList;
            }

            set
            {
                _PaymentStatusList = value;
            }
        }
        private static string _De;
        public static string Data
        {
            get
            {
                _De = "Rs.";
                return _De;
            }
            set
            {
                _De = value;
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
                        ["Packed"] = "success",
                        ["Delivered"] = "success",
                        ["Ordered"] = "danger",
                        ["Confirmed"] = "warning",
                        ["Dispatched"] = "info",
                        ["Cancelled"] = "danger"

                    };
                }
                return _OrderStatusTag;
            }

            set
            {
                _OrderStatusTag = value;
            }
        }
        private static List<SelectListItem> _modeList;
        public static List<SelectListItem> ModeList
        {
            get
            {
                if (_modeList == null)
                {
                    _modeList = new List<SelectListItem>
                    {
                        new SelectListItem { Value = "Cash", Text = "Cash" },
                        new SelectListItem { Value = "Card", Text = "Card Payment" },
                        new SelectListItem { Value = "GPay", Text = "GPay" },
                        new SelectListItem { Value = "PhonePe", Text = "PhonePe" },
                        new SelectListItem { Value = "PayTm", Text = "PayTm" }
                       //  new SelectListItem { Value = "PHOP", Text = "PhonePay",  },
                        // new SelectListItem { Value = "Paytm", Text = "Paytm" },
                        // new SelectListItem { Value = "MobiKwik", Text = "MobiKwik" },
                        // new SelectListItem { Value = "PayZ", Text = "PayZ" },
                        // new SelectListItem { Value = "YesPay", Text = "YesPay" },
                       //  new SelectListItem { Value = "OTH", Text = "Other Modes" },
                       // new SelectListItem { Value = "CrOD", Text = "CrOD" }
                    };
                }
                return _modeList;
            }

            set
            {
                _modeList = value;
            }
        }
    }
}
