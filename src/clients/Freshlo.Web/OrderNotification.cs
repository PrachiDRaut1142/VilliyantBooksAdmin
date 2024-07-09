using Freshlo.Repository;
using Freshlo.RI;
using Freshlo.SI;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using SignalR_SqlTableDependency.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web
{
    public class OrderNotification : Hub
    {
        OrderRepository orderRepository;
        public OrderNotification(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            orderRepository = new OrderRepository(connectionString);
        }
        
        public async Task SendOrderNotification()
        {
            var orderCount = orderRepository.GetOrders()[0].TokenNumber;
            await Clients.All.SendAsync("ReceiveOrderNotification", orderCount);
        }

    }
}
