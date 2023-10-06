using DbHelper.Model;
using DbHelper.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHelper.Service
{
    public class APPService
    {
        private readonly APPRepository _appRepository;
        private readonly UserRepository _userRepository;

        public APPService(APPRepository appRepository, UserRepository userRepository)
        {
            _appRepository = appRepository;
            _userRepository = userRepository;
        }

        public Task<dynamic> GetAppServerVersion(string appName)
        {
            return _appRepository.GetAppServerVersion(appName);
        }
    }
}
