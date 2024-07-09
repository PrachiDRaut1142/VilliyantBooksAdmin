using Freshlo.DomainEntities.Notification;
using Freshlo.RI;
using Freshlo.SI;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.Services
{
    public class NotificationService:INotificationSI
    {
        private readonly INotificationRI _notificationRI;
        public NotificationService(INotificationRI notificationRI)
        {
            _notificationRI = notificationRI;
        }
        public void Create(Notification info)
        {
            _notificationRI.Create(info);
        }
        public List<SelectListItem> GetCustomerList()
        {
            return _notificationRI.GetCustomerList();
        }
        public List<SelectListItem> GetCustomerContactList()
        {
            return _notificationRI.GetCustomerContactList();

        }
        public void SendSms(string message, string mobileNo, string senderId)
        {
            _notificationRI.SendSms(message, mobileNo, senderId);
        }
        public Task<List<SelectListItem>> GetCustomerListTrigger(int a, int b)
        {
            return Task.Run(() =>
            {
                return _notificationRI.GetCustomerTriggerList(a,b);
            });
        }
        public Task<List<SelectListItem>> GetCustomerContactListTrigger(int a, int b)
        {
            return Task.Run(() =>
            {
                return _notificationRI.GetCustomerContactTriggerList(a, b);
            });
        }
        public Task<bool> SendSmsP(string message, string mobileNo, string senderId)
        {
            return Task.Run(() =>
            {
                return _notificationRI.SendSmsp(message, mobileNo, senderId);
            });
        }
    }
}
