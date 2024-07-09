using Freshlo.DomainEntities;
using Freshlo.DomainEntities.Stock;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.SI
{
   public interface IStockSI
    {
         Task<List<Stock>> GetStockList(string itemName, string hub);
        Task<List<SelectListItem>> GetMainCategoryList(string id);
        Task<Stock> GetStock(string hubId);
        Task<List<Stock>> GetStockItemlist(string maincategory, string Category, int type, string hubId);

        Task<int> UpdateItemIsVisiable(Stock st);
        Task<int> UpdateStock(Stock st);
        Task<int> UpdatePrice(Stock st);
        Task<byte[]> ExportExcelofStock(string hubId, string role, string webRootPath, string maincategory, string Category, int type);
        List<SelectListItem> GetHeaderList(string rootFolder, string fileName);
        int InsertList(TblListcs list);
        DataTable ReadExcelData(TblListcs camp);
        string UploadStatgingListFromExcels(TblListcs camp);
        int UploadStaggingToStock(TblListcs camp);
        Task<TblListcs> GetCountDetail(TblListcs camp);
        Task<byte[]> ExportRejectedData(string hubId, string role, string webRootPath, int ListId);
        int DeleteStockStaggingData();
    }
}
