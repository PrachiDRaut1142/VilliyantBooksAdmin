using Freshlo.DomainEntities;
using Freshlo.RI;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.Repository
{
    public class DropDownRepository:DropDownRI
    {
        public DropDownRepository(IDbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }
        private IDbConfig _dbConfig { get; }
        public List<DropDown> OrderStatus()
        {
            List<DropDown> status = new List<DropDown>();
            string[] name = new string[] { "Ordered", "InProcess", "Packed", "Delivered", "Cancelled", "Confirmed", "Dispatched", "ReAttempt", "UnDelivered", "Refused", "Redispatched #2" };
            for (int i = 0; i < name.Length; i++)
            {
                status.Add(new DropDown
                {
                    OrderStatus = name[i],
                });
            }
            return status;
        }
        public  List<SelectListItem> Segment()
        {
            List<SelectListItem> segment = new List<SelectListItem>();
            string[] name = new string[] { "Hybrid", "Organic", "Home Made" };
            string[] value = new string[] { "1", "2", "3" };
            for (int i = 0; i < name.Length; i++)
            {
                segment.Add(new SelectListItem
                {
                    Text = name[i],
                    Value = value[i]
                });
            }
            return segment;
        }
        public List<SelectListItem> Measurement()
        {
            List<SelectListItem> measure = new List<SelectListItem>();
            string[] name = new string[] { "Units", "Gram", "Bundle", "Kg", "Liters", "Dozen", "Milligram", "Milliliter", "Packed", "Box" };
            string[] value = new string[] { "Ut", "gm", "bdl", "Kg", "Lt", "Doz", "mg", "ml", "pk", "bx" };
            for (int i = 0; i < name.Length; i++)
            {
                measure.Add(new SelectListItem
                {
                    Text = name[i],
                    Value = value[i],
                });
            }
            return measure;
        }
        public List<SelectListItem> offerType()
        {
            List<SelectListItem> offer = new List<SelectListItem>();
            string[] name = new string[] { "Regular Item", "Combo Pack", "Bulk Offer", "Promotional Offer", "Buy 1 Get 1", "Launch Offer", "Festival Offer" };
            string[] value = new string[] { "1", "2", "3", "4", "5", "6", "7" };

            for (int i = 0; i < name.Length; i++)
            {
                offer.Add(new SelectListItem
                {
                    Text = name[i],
                    Value = value[i],
                    //Tempcount = 5,
                });
            }
            return offer;
        }


        public List<SelectListItem> foodType()
        {
            List<SelectListItem> offer = new List<SelectListItem>();
            string[] name = new string[] { "Veg", "Non-Veg", "Not-Applicable"};
            string[] value = new string[] { "Veg", "Non-Veg", "NA"};

            for (int i = 0; i < name.Length; i++)
            {
                offer.Add(new SelectListItem
                {
                    Text = name[i],
                    Value = value[i],
                });
            }
            return offer;
        }

        public List<SelectListItem> foodsubType()
        {
            List<SelectListItem> offer = new List<SelectListItem>();
            string[] name = new string[] { "FreshVggie", "Chiken", "Mutton","Other" };
            string[] value = new string[] { "FreshVggie", "Chiken", "Mutton", "Other" };

            for (int i = 0; i < name.Length; i++)
            {
                offer.Add(new SelectListItem
                {
                    Text = name[i],
                    Value = value[i],
                });
            }
            return offer;
        }

    }
}
