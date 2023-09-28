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
        public Task<dynamic> GetUserById(string userCode)
        {
            return _userRepository.GetUserById(userCode);
        }
    }
}
