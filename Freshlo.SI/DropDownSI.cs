using Freshlo.DomainEntities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.SI
{
    public interface DropDownSI
    {
        List<DropDown> OrderStatus();
        Task<List<SelectListItem>> Segment();
        Task<List<SelectListItem>> Measurement();
        Task<List<SelectListItem>> offerType();
        Task<List<SelectListItem>> foodType();
        Task<List<SelectListItem>> foodsubType();


    }
}
