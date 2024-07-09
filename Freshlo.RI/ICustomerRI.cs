using Freshlo.DomainEntities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.RI
{
    public interface ICustomerRI
    {
        List<CustomerSalesHistory> GetCustomerHistory(string hubId,string role);
        int AddToWallet(Wallet info);
        List<CustomersAddress> GetAreawiseCustomer();
        CustomerSummaryCount GetAllCustomerOrderCount();
        CustomerSummaryCount GetAllnewCustomerCount(int filter);
        CustomerSalesHistory GetSalesSummary(string Id);
        List<Sales> GetSalesOrderList(string CustId);
        Customer GetCustomerDetail(string id);
        List<Customer> GetCustomerDetaillist(string id);
        List<Customer> GetCustomerList();
        List<Customer> GetCustomerOrderlist(int a, int b);


        CustomerSummaryCount GetAllnewCustomerCountHub(int filter, string Hub);
        CustomerSummaryCount GetAllnewCustomerCountZip(int filter, string ZipCode);
        byte[] FilterExcelofCustomer(string branch, string role, string webRootPath);
        int AddAddress(Customer cust);
        int AddsecAddress(Customer cust);
        int CreateCustomer(Customer cust);
    

        int editaddress(Customer cust);
        bool delcustomer(string Id);
        Task<Customer> GetCustomerDataId(string Type, string custId);
        int EditAddress(Customer cust);
    }
}
