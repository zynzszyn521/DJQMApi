using DbHelper.Model;
using DbHelper.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHelper.Service
{
    public class VipPriceService
    {
        private readonly VipPriceRepository _userNoteRepository;

        public VipPriceService(VipPriceRepository userNoteRepository)
        {
            _userNoteRepository = userNoteRepository;
        }

        public Task<ReturnResult> SaveVipPrice(VipPriceModel model)
        {
            return _userNoteRepository.SaveVipPrice(model);
        }
        public Task<dynamic> GetVipPrice(string userCode)
        {
            return _userNoteRepository.GetVipPrice(userCode);
        }
    }
}
