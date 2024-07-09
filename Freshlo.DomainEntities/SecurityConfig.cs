using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities
{
    public class SecurityConfig
    {
        public int Unique_Password_Count { get; set; }
        public int Password_Length { get; set; }
        public int Password_Expiry_Day { get; set; }
        public int Session_Expiry_Hours { get; set; }
        public bool Remember_Password { get; set; }
        public bool Allow_Special_Character { get; set; }
        public bool Alpha_Numeric { get; set; }
        public bool Check_Capital { get; set; }
        public int Login_Attempt { get; set; }
        public int? Modified_By { get; set; }
        public DateTime? Modified_Date { get; set; }


    }
}
