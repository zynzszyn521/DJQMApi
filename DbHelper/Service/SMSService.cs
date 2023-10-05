using DbHelper.Model;
using DbHelper.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHelper.Service
{
    public class SMSService
    {
        private readonly SMSRepository _smsRepository;
        private readonly UserRepository _userRepository;

        public SMSService(SMSRepository smsRepository, UserRepository userRepository)
        {
            _smsRepository = smsRepository;
            _userRepository = userRepository;
        }

        public async Task<ReturnResult> SendSMS(SMSModel model)
        {
            Random rm = new Random();
            model.smsCode = Convert.ToString(rm.Next(1000, 9999));
            //如果用户没有在基本表则插入
            UserModel userModel = new UserModel()
            {
                userCode = model.phoneNumber,
                phoneNumber = model.phoneNumber,
            };
            await _userRepository.SaveUser(userModel);
            //保存短信记录
            ReturnResult saveResult = await _smsRepository.SaveSMSLog(model);
            if (saveResult.successed)
            {
                string strSendResult = SMSLib.AliSMS.CallMessage(model.phoneNumber, model.smsCode);
                if (strSendResult.StartsWith("TRUE"))
                {
                    return new ReturnResult() { successed = true, msg = model.smsCode };
                }
                else
                {
                    return new ReturnResult() { successed = false, msg = strSendResult.Replace("FALSE^", "") };
                }
            }
            else
            {
                return saveResult;
            }
        }
    }
}
