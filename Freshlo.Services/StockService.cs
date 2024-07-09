using Freshlo.DomainEntities.Stock;
using Freshlo.RI;
using Freshlo.SI;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using Freshlo.DomainEntities;
using System.Data;

namespace Freshlo.Services
{
    public class StockService : IStockSI
    {
        private readonly IStockRI _stockRI;
        public StockService(IStockRI stockRI)
        {
            _stockRI = stockRI;
        }

        public Task<Stock> GetStock(string hubId)
        {
            return Task.Run(() =>
            {
                return _stockRI.GetStock(hubId);
            });
        }
        public Task<List<Stock>> GetStockItemlist(string maincategory, string Category, int type, string hubId)
        {
            return Task.Run(() =>
            {
                return _stockRI.GetStockItemlist(maincategory, Category, type, hubId);
            });
        }
        public async Task<List<Stock>> GetStockList(string itemName, string hub)
        {
            return await _stockRI.GetStockList(itemName, hub);
        }
        public Task<List<SelectListItem>> GetMainCategoryList(string id)
        {
            return Task.Run(() =>
            {
                return _stockRI.GetMainCategoryList(id);
            });
        }

        public Task<int> UpdateItemIsVisiable(Stock st)
        {
            return Task.Run(() =>
            {
                return _stockRI.UpdateItemIsVisiable(st);
            });
        }

        public Task<int> UpdateStock(Stock st)
        {
            return Task.Run(() =>
            {
                return _stockRI.UpdateStock( st);
            });
        }

        public Task<int> UpdatePrice(Stock st)
        {
            return Task.Run(() =>
            {
                return _stockRI.UpdatePrice( st);
            });
        }
        public Task<byte[]> ExportExcelofStock(string hubId, string role, string webRootPath, string maincategory, string Category, int type)
        {
            return Task.Run(() =>
            {
                return _stockRI.ExportExcelofStock(hubId, role, webRootPath,  maincategory,  Category,  type);
            });
        }
        public List<SelectListItem> GetHeaderList(string rootFolder, string fileName)
        {
              return _stockRI.GetHeaderList( rootFolder, fileName);
        }
        public int InsertList(TblListcs list)
        {
            return _stockRI.InsertList( list);
        }
        public DataTable ReadExcelData(TblListcs camp)
        {
            return _stockRI.ReadExcelData(camp);
        }

        public string UploadStatgingListFromExcels(TblListcs camp)
        {
            return _stockRI.UploadStatgingListFromExcels(camp);
        }
        public int UploadStaggingToStock(TblListcs camp)
        {
            return _stockRI.UploadStaggingToStock(camp);
        }
        public Task<TblListcs> GetCountDetail(TblListcs camp)
        {
            return Task.Run(() =>
            {
                return _stockRI.GetCountDetail(camp);
            });
        }
        public Task<byte[]> ExportRejectedData(string hubId, string role, string webRootPath, int ListId)
        {
            return Task.Run(() =>
            {
                return _stockRI.ExportRejectedData( hubId, role, webRootPath, ListId);
            });
        }
        public int DeleteStockStaggingData() { 
            return _stockRI.DeleteStockStaggingData();
        }
    }
}
