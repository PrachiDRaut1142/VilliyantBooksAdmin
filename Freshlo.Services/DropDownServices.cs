using Freshlo.DomainEntities;
using Freshlo.RI;
using Freshlo.SI;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.Services
{
    public class DropDownServices:DropDownSI
    {
        private readonly DropDownRI _DropDownRI;
        public DropDownServices(DropDownRI DropDownRI)
        {
            _DropDownRI = DropDownRI;
        }
        public List<DropDown> OrderStatus()
        {
            return _DropDownRI.OrderStatus();
        }
        public Task<List<SelectListItem>> Segment()
        {
            return Task.Run(() =>
            {
                return _DropDownRI.Segment();
            });
                
        }
        public Task<List<SelectListItem>> Measurement()
        {
            return Task.Run(() =>
            {
                return _DropDownRI.Measurement();
            });

        }
        public Task<List<SelectListItem>> offerType()
        {
            return Task.Run(() =>
            {
                return _DropDownRI.offerType();
            });
        }

        public Task<List<SelectListItem>> foodType()
        {
            return Task.Run(() =>
            {
                return _DropDownRI.foodType();
            });
        }

        public Task<List<SelectListItem>> foodsubType()
        {
            return Task.Run(() =>
            {
                return _DropDownRI.foodsubType();
            });
        }

    }
}
