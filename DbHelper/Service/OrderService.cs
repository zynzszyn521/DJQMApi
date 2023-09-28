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
        private readonly OrderRepository _articleRepository;

        public OrderService(OrderRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public Task<ReturnResult> SaveOrder(OrderModel model)
        {
            return _articleRepository.SaveOrder(model);
        }
        public Task<dynamic> GetOrderList(string userCode)
        {
            return _articleRepository.GetOrderList(userCode);
        }
    }
}
