using Freshlo.DomainEntities;
using Freshlo.RI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Freshlo.Repository
{
    public class SystemConfigRepository:ISystemConfigRI
    {
        public SystemConfigRepository(IDbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }
        private IDbConfig _dbConfig { get; }     

        public SecurityConfig GetSecurityConfig()
        {
            SecurityConfig securityConfig = null;
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[SystemConfig_GetSecurityConfig]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        securityConfig = new SecurityConfig
                        {
                            Unique_Password_Count = Convert.ToInt32(rd["Unique_Password_Count"]),
                            Password_Length = Convert.ToInt32(rd["Password_Length"]),
                            Password_Expiry_Day = Convert.ToInt32(rd["Password_Expiry_Day"]),
                            Session_Expiry_Hours = Convert.ToInt32(rd["Session_Expiry_Hours"]),
                            Remember_Password = Convert.ToBoolean(rd["Remember_Password"]),
                            Allow_Special_Character = Convert.ToBoolean(rd["Allow_Special_Character"]),
                            Alpha_Numeric = Convert.ToBoolean(rd["Alpha_Numeric"]),
                            Check_Capital = Convert.ToBoolean(rd["Check_Capital"]),
                            Login_Attempt = Convert.ToInt32(rd["Login_Attempt"]),
                            Modified_By = Convert.ToInt32(rd["Modified_By"] == DBNull.Value ? null : rd["Modified_By"]),
                            Modified_Date = Convert.ToDateTime(rd["Modified_Date"] == DBNull.Value ? null : rd["Modified_Date"])
                        };
                    }
                    return securityConfig;
                }
            }
        }

        public Emailconfig GetEmailConfig()
        {
            Emailconfig emailConfig = null;
            
            using (SqlConnection con = new SqlConnection(_dbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[SystemConfig_GetEmailConfig]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        emailConfig = new Emailconfig
                        {
                            Id = Convert.ToInt32(rd["Email_ID"]) ,
                            DisplayName = rd["Display_Name"] as string,
                            EmailAddress = rd["Email_Address"] as string,
                            EmailType = rd["Email_Type"] as string,
                            UserName = rd["UserName"] as string,
                            Password = rd["Password"] as string,
                            IncomingMainServer = rd["Incoming_Main_Server"] as string,
                            IncomingPort = rd["Incoming_Port"] as string,
                            OutgoingMainServer = rd["Outgoing_Main_Server"] as string,
                            OutgoingPort = rd["Out_Going_Port"] as string,
                            IsSsl = Convert.ToBoolean(rd["IsSpa_Or_Spl"]),
                            CreatedBy = Convert.ToInt32(rd["Created_By"]),
                            CreatedOn = Convert.ToDateTime(rd["Created_On"]),
                            ModifiedBy = MappingHelpers.IntegerGetValue(rd["Modified_By"]),
                            ModifiedOn = MappingHelpers.DateTimeGetValue(rd["Modified_On"])
                        };
                    }
                    return emailConfig;
                }
            }
        }
    }
}
