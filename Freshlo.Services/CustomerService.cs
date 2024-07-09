using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Freshlo.DomainEntities;
using Freshlo.RI;
using Freshlo.SI;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Freshlo.Services
{
    public class CustomerService : ICustomerSI
    {
        private readonly ICustomerRI _customerRI;

        public CustomerService(ICustomerRI customerRI)
        {
            _customerRI = customerRI;
        }
        public Task<List<CustomerSalesHistory>> GetCustomerHistory(string hubId, string role)
        {
            return Task.Run(() =>
            {
                return _customerRI.GetCustomerHistory(hubId, role);
            });
        }
        public Task<int> AddToWallet(Wallet info)
        {
            return Task.Run(() =>
            {
                return _customerRI.AddToWallet(info);
            });
        }

        public Task<List<CustomersAddress>> GetAreawiseCustomer()
        {
            return Task.Run(() =>
            {
                return _customerRI.GetAreawiseCustomer();
            });
        }

        public Task<CustomerSummaryCount> GetAllCustomerOrderCount()
        {
            return Task.Run(() =>
            {
                return _customerRI.GetAllCustomerOrderCount();
            });
        }

        public Task<CustomerSummaryCount> GetAllnewCustomerCount(int filter)
        {
            return Task.Run(() =>
            {
                return _customerRI.GetAllnewCustomerCount(filter);
            });
        }
        public Task<CustomerSalesHistory> GetSalesSummary(string Id)
        {
            return Task.Run(() =>
            {
                return _customerRI.GetSalesSummary(Id);
            });
        }
        public Task<List<Sales>> GetSalesOrderList(string CustId)
        {
            return Task.Run(() =>
              {
                  return _customerRI.GetSalesOrderList(CustId);
              });
        }
        public Task<List<Customer>> GetCustomerDetaillist(string id)
        {
            return Task.Run(() =>
              {
                  return _customerRI.GetCustomerDetaillist(id);
              });
        }
        public Task<Customer> GetCustomerDetail(string id)
        {
            return Task.Run(() =>
            {
                return _customerRI.GetCustomerDetail(id);
            });
        }

        public Task<List<Customer>> GetCustomerList()
        {
            return Task.Run(() =>
            {
                return _customerRI.GetCustomerList();
            });
        }

        public Task<List<Customer>> GetCustomerOrderlist(int a, int b)
        {
            
                return Task.Run(() =>
                {
                    return _customerRI.GetCustomerOrderlist(a, b);
                });
            
        }

        public Task<CustomerSummaryCount> GetAllnewCustomerCountHubWise(int filter, string Hub)
        {
            return Task.Run(() =>
            {
                return _customerRI.GetAllnewCustomerCountHub(filter, Hub);
            });
        }

        public Task<CustomerSummaryCount> GetAllnewCustomerCountZipWise(int filter, string ZipCode)
        {
            return Task.Run(() =>
            {
                return _customerRI.GetAllnewCustomerCountZip(filter, ZipCode);
            });
        }

        public Task<byte[]> FilterExcelofCustomer(string branch, string role, string webRootPath)
        {
            return Task.Run(() =>
            {
                return _customerRI.FilterExcelofCustomer(branch, role, webRootPath);
            });
        }
        public int CreateCustomer(Customer cust)
        {
            return _customerRI.CreateCustomer(cust);
        }

        public int AddAddress(Customer cust)
        {
            return _customerRI.AddAddress(cust);

        }

        public Task<bool> delcustomer(string Id)
        {
            return Task.Run(() =>
            {
                return _customerRI.delcustomer(Id);
            });
        }

        public int editaddress(Customer cust)
        {
            return _customerRI.editaddress(cust);
        }

        public async Task<Customer> GetCustomerDataId(string Type, string custId)
        {
            return await _customerRI.GetCustomerDataId(Type, custId);
        }

        public int AddsecAddress(Customer cust)
        {
            return _customerRI.AddsecAddress(cust);

        }

        public Task<int> EditAddress(Customer cust)
        {
            return Task.Run(() =>
            {
                return _customerRI.EditAddress(cust);
            });
        }
    }
}
