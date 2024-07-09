using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities
{
    public class _HeaderModalVM
    {
        public string FileName { get; set; }
        public List<SelectListItem> HeaderList { get; set; }
    }
}
