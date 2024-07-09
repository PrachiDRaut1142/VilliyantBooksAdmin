using Freshlo.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace Freshlo.Web.Helpers
{
    public class SMSHelper
    {
        public static async Task<string> SendSMS(string MblNo, string Msg)
        {
            string MainUrl = "SMSAPIURL"; //Here need to give SMS API URL
            string UserName = "username"; //Here need to give username
            string Password = "Password"; //Here need to give Password
            string SenderId = "SenderId";
            string strMobileno = MblNo;

            string URL = MainUrl + "username=" + UserName + "&msg_token=" + Password + "&sender_id=" + SenderId + "&message=" + HttpUtility.UrlEncode(Msg).Trim() + "&mobile=" + strMobileno.Trim() + "";
            string strResponce = GetResponse(URL);
            string msg = "";
            if (strResponce.Equals("Fail"))
            {
                msg = "Fail";
            }
            else
            {
                msg = strResponce;
            }
            return msg;

        }
        public static string GetResponse(string smsURL)
        {
            try
            {
                WebClient objWebClient = new WebClient();
                System.IO.StreamReader reader = new System.IO.StreamReader(objWebClient.OpenRead(smsURL));
                string ResultHTML = reader.ReadToEnd();
                return ResultHTML;
            }
            catch (Exception)
            {
                return "Fail";
            }
        }
        public static async Task<string> SendEmailSMSAsync(TableInfo info)
        {
            //string URL = "http://login.dndopensms.com/api/sendhttp.php?authkey=153831AHeUUWRSe5927bde0&mobiles=" + val.ContactNo + "&message=Your%20OTP%20" + userInfo.OTP + "&sender=Tokens&route=4";
            string URL = "http://login.dndopensms.com/api/sendhttp.php?authkey=153831AHeUUWRSe5927bde0&mobiles=" + info.custNumber + "&message=Your%2BOTP%2Bis%2B" + info.OTP + ".%2BPlease%2Bdo%2Bnot%2Bshare%2Bit%2Bwith%2Banyone.%0ABest%2C%0AArabian%2BDastar&sender=Dastar&route=4&unicode=1&country=91&DLT_TE_ID=1207163238127923199";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URL);
                //HTTP GET
                var responseTask = await client.GetAsync("");
                var result = responseTask;
            }
            //abdulintojob@gmail.com;salam_tata@rediffmail.com
            if (info.custEmail != "")
            {
                using (MailMessage mm = new MailMessage("info.qbuddy@automatebuddy.com", info.custEmail))
                {
                    mm.Subject = "OTP for Arabian Dastar...";
                    mm.Body = "<strong>Dear " + info.custName + "</strong>,<br/>   Your OTP is " + info.OTP +
                            @" Please do not share it with anyone.<br/>
                        <br/><strong>Best</strong>,<br/>
                        <strong>Arabian Dastar</strong>";
                    mm.IsBodyHtml = true;
                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = "mail.automatebuddy.com";
                        smtp.EnableSsl = false;
                        NetworkCredential NetworkCred = new NetworkCredential("info.qbuddy@automatebuddy.com", "Pass@123");
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        smtp.Send(mm);
                        return "Email sent";
                    }
                }

            }
            return "Email Sent";
        }


    }
}
