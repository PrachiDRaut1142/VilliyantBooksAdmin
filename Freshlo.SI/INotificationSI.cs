using Freshlo.DomainEntities.Notification;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.SI
{
    public interface INotificationSI
    {
        void Create(Notification info);
        List<SelectListItem> GetCustomerList();
        List<SelectListItem> GetCustomerContactList();
        void SendSms(string message, string mobileNo, string senderId);
        Task<List<SelectListItem>> GetCustomerListTrigger(int a, int b);
        Task<List<SelectListItem>> GetCustomerContactListTrigger(int a, int b);
        Task<bool> SendSmsP(string message, string mobileNo, string senderId);
    }
}
