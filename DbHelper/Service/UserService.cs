using DbHelper.Model;
using DbHelper.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHelper.Service
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<ReturnResult> SaveUser(UserModel model)
        {
            return _userRepository.SaveUser(model);
        }
        public async Task<ReturnResult> UpdateVipUser(UserVipHModel model)
        {
            await _userRepository.SaveUserVipH(model);
            UserModel userModel = (await GetUserById(model.userCode).ConfigureAwait(false)).FirstOrDefault();
            DateTime dBeginTime = DateTime.Now;
            if (userModel.expirationTime != null)
            {
                //如果到期时间晚于现在时间，则累加
                if (userModel.expirationTime > DateTime.Now)
                {
                    dBeginTime = (DateTime)userModel.expirationTime;
                }
            }
            userModel.vipFlag = 1;
            userModel.expirationTime = dBeginTime.AddMonths(model.monthLength);
            return await _userRepository.UpdateVipUser(userModel);

        }
        public Task<IEnumerable<UserModel>> GetUserById(string userCode)
        {
            return _userRepository.GetUserById(userCode);
        }
    }
}
