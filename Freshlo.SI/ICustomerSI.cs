using Freshlo.DomainEntities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.SI
{
   public  interface ICustomerSI
    {
        Task<List<CustomerSalesHistory>> GetCustomerHistory(string hubId,string role);
        Task<int> AddToWallet(Wallet info);
        Task<List<CustomersAddress>> GetAreawiseCustomer();
        Task<CustomerSummaryCount> GetAllCustomerOrderCount();
        Task<CustomerSummaryCount> GetAllnewCustomerCount(int filter);
        Task<CustomerSalesHistory> GetSalesSummary(string Id);
        Task<Customer> GetCustomerDetail(string id);
        Task<List<Customer>> GetCustomerDetaillist (string id);
        Task<bool> delcustomer(string Id);
        Task<List<Sales>>GetSalesOrderList(string CustId);
        Task<List<Customer>> GetCustomerList();

        Task<List<Customer>> GetCustomerOrderlist(int a, int b);

        Task<CustomerSummaryCount> GetAllnewCustomerCountHubWise(int filter, string Hub);
        Task<CustomerSummaryCount> GetAllnewCustomerCountZipWise(int filter, string ZipCode);

        Task<byte[]> FilterExcelofCustomer(string branch, string role, string webRootPath);
        int CreateCustomer(Customer cust);
        int AddAddress(Customer cust);
        int AddsecAddress(Customer cust);
       
        int editaddress(Customer cust);
        Task<Customer> GetCustomerDataId(string Type, string custId);

        Task<int> EditAddress(Customer cust);
    }
}
