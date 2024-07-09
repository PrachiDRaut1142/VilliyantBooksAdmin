using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.DomainEntities
{
    public class BusinessInfo
    {
        public int hotel_id { get; set; }
        public string AnnouncemntMessage { get; set; }
        public string hotel_name { get; set; }
        public int master_loginid { get; set; }
        public string master_password { get; set; }
        public string admin_name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string contact_number { get; set; }
        public string alternate_contactnumber { get; set; }
        public string email { get; set; }
        public string business_emailaddress { get; set; }
        public string logo_url { get; set; }
        public string secondarylogo_url { get; set; }
        public string printlogo_url { get; set; }
        public string website { get; set; }
        public string bussiness_type { get; set; }
        public int hotel_status { get; set; }
        public int is_multibranch { get; set; }
        public int OTP { get; set; }
        public int hosting_type { get; set; }
        public string subdomain_name { get; set; }
        public int subscription_status { get; set; }
        public int subscription_type { get; set; }
        public DateTime subscription_expirydate { get; set; }
        public string sms_header { get; set; }
        public string banner_text { get; set; }
        public DateTime created_on { get; set; }
        public int created_by { get; set; }
        public DateTime modified_on { get; set; }
        public int modified_by { get; set; }
        public int bussiness_status { get; set; }
        public string currency { get; set; }
        public string Country { get; set; }
        public string Language { get; set; }
        public IFormFile UploadImage { get; set; }
        public IFormFile SecondUploadImage { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }

        public string city { get; set; }
        public string state { get; set; }
        public string postalCode { get; set; }
        public string thankhyouMessage { get; set; }
        public string splashScreenMessage { get; set; }
        public List<CurrencyMST> getCurrencyList { get; set; }
        public string symbol { get; set; }
        public  string ShortCode { get; set; }
        public string Currency { get; set; }
        public List<TimeZoneDetails> getTimezoneList { get; set; }
        public int TimeId { get; set; }
        public string legalbusinessName { get; set; }
        public string aliyunPath { get; set; }
        public string dbName { get; set; }

    }

}
