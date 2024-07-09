using Freshlo.DomainEntities.Notification;
using Freshlo.RI;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Freshlo.Repository
{
    public class NotificationRepository : INotificationRI
    {
        public NotificationRepository(IDbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }
        private IDbConfig _dbConfig { get; }
        public void Create(Notification info)
        {

            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "[dbo].[Notification_Create]";
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                DataTable odt = new DataTable();
                odt.Columns.Add("NotifyTo", typeof(string));
                odt.Columns.Add("Notification_type", typeof(string));
                odt.Columns.Add("Notification_Title", typeof(string));
                odt.Columns.Add("Notification_Desc", typeof(string));
                odt.Columns.Add("Created_By", typeof(string));
                if (info.Type != null)
                    for (var i = 0; i < info.Customer.Length; i++)
                    {
                        odt.Rows.Add(info.Customer[i], info.Type, info.Title, info.Description, info.Created_By);
                    }
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ListType", SqlDbType.Structured).Value = odt;
                cmd.ExecuteNonQuery();

            }
        }
        public List<SelectListItem> GetCustomerList()
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Customer_GetNameList]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        selectList.Add(new SelectListItem
                        {
                            Text = Convert.ToString(rd[1]),
                            Value = Convert.ToString(rd[0]),

                        });
                    }
                    return selectList;
                }
            }
        }
        public List<SelectListItem> GetCustomerContactList()
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Customer_GetListbyContactNo]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        selectList.Add(new SelectListItem
                        {
                            Text = Convert.ToString(rd["Name"]),
                            Value = Convert.ToString(rd["ContactNo"]),

                        });
                    }
                    return selectList;
                }
            }
        }
        public void SendSms(string message, string mobileNo, string senderId)
        {
            try
            {
                var country = "0";
                //var country = "91";
                long mob = 0;
                if (mobileNo.Contains("+1"))
                {
                    country = "1";
                }
                if (!mobileNo.Contains("+") && mobileNo.Length == 10)
                {
                    mobileNo = "91" + mobileNo;
                    mob = Convert.ToInt64(mobileNo);
                }
                //Old SMS Link
                //var requrl = "http://dndopensms.com/api/sendhttp.php?authkey=102224AHzesIsKJn85693de46&mobiles=" + mobileNo + "&message=" + message + "&sender=" + senderId + "&route=4";
                //New SMS Link
                // var requrl = "http://dndopensms.com/api/sendhttp.php?authkey=153831AHeUUWRSe5927bde0&mobiles=" + mobileNo + "&message=" + message + "&sender=" + senderId + "&route=4&unicode=1&country=" + country;
                // Again New SMS Link
                // var requrl = "http://login.dndopensms.com/api/sendhttp.php?authkey=153831AHeUUWRSe5927bde0&mobiles=" + mob + "&message=" + message + "&sender=" + senderId + "&route=" + 4 + "&unicode=" + 1 + "&country=" + country;

                var requrl = "http://login.dndopensms.com/api/sendhttp.php?authkey=153831AHeUUWRSe5927bde0&mobiles="
                   + mobileNo + "&message=" + message + "&sender=" + senderId + "&route=4&unicode=1&country=" + country;


                var myReq = (HttpWebRequest)WebRequest.Create(requrl);
                var myResp = (HttpWebResponse)myReq.GetResponse();
                // ReSharper disable once AssignNullToNotNullAttribute
                var respStreamReader = new StreamReader(myResp.GetResponseStream());
                var responseString = respStreamReader.ReadToEnd();
                respStreamReader.Close();
                myResp.Close();
                //return true;


                //HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(requrl);
                //UTF8Encoding encoding = new UTF8Encoding();
                //byte[] data = encoding.GetBytes(requrl.ToString());
                //httpWReq.Method = "POST";
                //httpWReq.ContentType = "application/x-www-form-urlencoded";
                //httpWReq.ContentLength = data.Length;
                //using (Stream stream = httpWReq.GetRequestStream())
                //{
                //    stream.Write(data, 0, data.Length);
                //}
                //HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                //StreamReader reader = new StreamReader(response.GetResponseStream());
                //string responseString = reader.ReadToEnd();
                //reader.Close();
                //response.Close();


            }
            catch (Exception ex)
            {

            }
        }
        public List<SelectListItem> GetCustomerTriggerList(int a, int b)
        {
            List<SelectListItem> getCustomerlist = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Notification_CustomerDropdown]", con))
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@a", SqlDbType.Int).Value = a;
                cmd.Parameters.Add("@b", SqlDbType.Int).Value = b;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        string text = Convert.ToString(rd["Name"]);
                        string Id = Convert.ToString(rd["CustomerId"]);
                        getCustomerlist.Add(new SelectListItem()
                        {
                            Text = text,
                            Value = Id,
                        });
                    }
                    return getCustomerlist;
                }
            }

        }
        public List<SelectListItem> GetCustomerContactTriggerList(int a, int b)
        {

            List<SelectListItem> getCustomerlist = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Notification_CustomerContactDropdown]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@a", SqlDbType.Int).Value = a;
                cmd.Parameters.Add("@b", SqlDbType.Int).Value = b;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        getCustomerlist.Add(new SelectListItem
                        {
                            Text = Convert.ToString(rd["Name"]),
                            Value = Convert.ToString(rd["ContactNo"]),

                        });
                    }
                    return getCustomerlist;
                }
            }
        }
        public async Task<bool> SendSmsp(string message, string mobileNo, string senderId)
        {
            
                try
                {
                    var country = "91";
                    if (mobileNo.Contains("+1"))
                    {
                        country = "1";
                    }
                    if (!mobileNo.Contains("+") && mobileNo.Length == 10)
                    {
                        mobileNo = "91" + mobileNo;
                    }
                    //Old SMS Link
                    //var requrl = "http://dndopensms.com/api/sendhttp.php?authkey=102224AHzesIsKJn85693de46&mobiles=" + mobileNo + "&message=" + message + "&sender=" + senderId + "&route=4";
                    //New SMS Link
                    //var requrl = "http://dndopensms.com/api/sendhttp.php?authkey=153831AHeUUWRSe59927bde0&mobiles=" + mobileNo + "&message=" + message + "&sender=" + senderId + "&route=4&unicode=1&country=" + country;
                    var requrl = "http://login.dndopensms.com/api/sendhttp.php?authkey=153831AHeUUWRSe5927bde0&mobiles="
                            + mobileNo + "&message=" + message + "&sender=" + senderId + "&route=4&unicode=1&country=" + country;
                    var myReq = (HttpWebRequest)WebRequest.Create(requrl);
                    var myResp = (HttpWebResponse)await myReq.GetResponseAsync();
                    // ReSharper disable once AssignNullToNotNullAttribute
                    var respStreamReader = new StreamReader(myResp.GetResponseStream());
                    var responseString = respStreamReader.ReadToEnd();
                    respStreamReader.Close();
                    myResp.Close();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }

        }
    }

