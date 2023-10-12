using DbHelper.Model;
using DbHelper.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHelper.Service
{
    public class UserOrderService
    {
        private readonly UserOrderRepository _userNoteRepository;

        public UserOrderService(UserOrderRepository userNoteRepository)
        {
            _userNoteRepository = userNoteRepository;
        }

        public Task<ReturnResult> SaveUserOrder(UserOrderModel model)
        {
            return _userNoteRepository.SaveUserOrder(model);
        }
        public Task<dynamic> GetUserOrder(string userCode, string orderType, string productId)
        {
            return _userNoteRepository.GetUserOrder(userCode, orderType, productId);
        }
    }
}
