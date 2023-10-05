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

        public SMSService(SMSRepository smsRepository)
        {
            _smsRepository = smsRepository;
        }

        public async Task<ReturnResult> SendSMS(SMSModel model)
        {
            Random rm = new Random();
            model.smsCode = Convert.ToString(rm.Next(1000, 9999));
            //先保存到数据库
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
                     return new ReturnResult() { successed = false, msg = strSendResult.Replace("FALSE^","") };
                }
            }
            else
            {
                return saveResult;
            }
        }
    }
}
