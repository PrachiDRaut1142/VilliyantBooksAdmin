using Freshlo.DomainEntities.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SignalR_SqlTableDependency.Repositories
{
    public class OrderRepository
    {
        string connectionString;

        public OrderRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<SaleOrderss> GetOrders()
        {
            List<SaleOrderss> countData = new List<SaleOrderss>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("select count(*) as TotalCount from[dbo].[SaleOrderss] where[OrderdStatus] = 'Ordered' and[Source] = 'Mob' and Comment !='viewed'", con))
                {
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            countData.Add(new SaleOrderss
                            {
                                TokenNumber = Convert.ToInt32(sdr["TotalCount"])
                            });

                        }
                        return countData;
                    }
                }
            }
        }

    }
}
