using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities.Notification
{
    public class Notification
    {
        public int Id { get; set; }
        public string NotifyTo { get; set; }
        public int RelatedTo { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created_On { get; set; }
        public string Created_By { get; set; }
        public int Is_New { get; set; }
        public IFormFile UploadImage { get; set; }
        public string[] Customer { get; set; }
        public string senderId { get; set; }
        public int NotificationType { get; set; }
        public string[] CustomerContact { get; set; }
        public string Aliyunkey { get; set; }

    }
}
