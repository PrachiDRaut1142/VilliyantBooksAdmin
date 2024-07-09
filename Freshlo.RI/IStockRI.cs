using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Stock;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.RI
{
   public interface IStockRI
    {
       Task<List<Stock>> GetStockList(string itemName, string hub);
       Stock GetStock(string hubId);
       List<Stock> GetStockItemlist(string maincategory,string Category ,int type, string hubId);
        List<SelectListItem> GetMainCategoryList(string id);
        List<SelectListItem> GetHeaderList(string rootFolder, string fileName);

        int UpdateItemIsVisiable(Stock st);
        int UpdateStock(Stock st);
        int UpdatePrice(Stock st);
        byte[] ExportExcelofStock(string hubId, string role, string webRootPath, string maincategory, string Category, int type);
        int InsertList(TblListcs list);
        DataTable ReadExcelData(TblListcs camp);
        string UploadStatgingListFromExcels(TblListcs camp);
        int UploadStaggingToStock(TblListcs camp);
        Task<TblListcs> GetCountDetail(TblListcs camp);
        byte[] ExportRejectedData(string hubId, string role, string webRootPath, int ListId);
        int DeleteStockStaggingData();
    }
}
