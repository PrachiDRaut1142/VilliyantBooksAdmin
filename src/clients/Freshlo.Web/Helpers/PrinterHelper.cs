using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Helpers
{
    public class PrinterHelper
    {
        // Where the items from the DB will be kept
        public Dictionary<int, string> PrintList { get; set; }

        // Used to store the users selected option
        public int SelectedPrinter { get; set; }

        // A constructor to be called when the page renders
        public PrinterHelper()
        {
            PrinterList();
        }

        public void PrinterList()
        {
            // 1. Grab your items from the database, store it within a databale
            // 2. Loop through the datatable and add each row to the list

            PrintList = new Dictionary<int, string>();

            //foreach (DataRow row in dt.Rows)
            //{
            //    PrintList.Add(Convert.ToInt32(row["ID"]), row["country"].ToString());
            //}
        }
    }
}
