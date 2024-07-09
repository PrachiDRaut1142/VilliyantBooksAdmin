using Freshlo.DomainEntities.Notification;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.RI
{
   public interface INotificationRI
    {
        void Create(Notification info);
        List<SelectListItem> GetCustomerList();
        List<SelectListItem> GetCustomerContactList();
        void SendSms(string message, string mobileNo, string senderId);
        List<SelectListItem> GetCustomerTriggerList(int a, int b);
        List<SelectListItem> GetCustomerContactTriggerList(int a, int b);
        Task<bool> SendSmsp(string message, string mobileNo, string senderId);


    }
}
