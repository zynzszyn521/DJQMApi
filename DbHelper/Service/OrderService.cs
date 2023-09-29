using DbHelper.Model;
using DbHelper.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHelper.Service
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;

        public OrderService(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Task<ReturnResult> SaveOrder(OrderModel model)
        {
            return _orderRepository.SaveOrder(model);
        }
        public Task<dynamic> GetOrderList(string userCode)
        {
            return _orderRepository.GetOrderList(userCode);
        }
    }
}
