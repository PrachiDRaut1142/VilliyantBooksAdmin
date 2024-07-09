using Freshlo.DomainEntities;
using Freshlo.DomainEntities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;

namespace Freshlo.Web.SuscribeTableDependencies
{
    public class SubscribeOrderTableDependency : ISubscribeTableDependency
    {
        SqlTableDependency<SaleOrderss> tableDependency;
        OrderNotification notifyHub;

        public SubscribeOrderTableDependency(OrderNotification notification)
        {
            this.notifyHub = notification;
        }

        public void SubscribeTableDependency(string connectionString)
        {
            tableDependency = new SqlTableDependency<SaleOrderss>(connectionString);
            tableDependency.OnChanged += TableDependency_OnChanged;
            tableDependency.OnError += TableDependency_OnError;
            tableDependency.Start();
        }

        private void TableDependency_OnError(object sender, ErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(SaleOrderss)} SqlTableDependency error: {e.Error.Message}");
        }

        private async void TableDependency_OnChanged(object sender, RecordChangedEventArgs<SaleOrderss> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
               await notifyHub.SendOrderNotification();
            }
        }
    }
}
