using Freshlo.DomainEntities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.RI
{
    public interface DropDownRI
    {
       List<DropDown> OrderStatus();
        List<SelectListItem> Segment();
        List<SelectListItem> Measurement();
        List<SelectListItem> offerType();
        List<SelectListItem> foodType();
        List<SelectListItem> foodsubType();


    }
}
