using Dapper;
using DbHelper.DbCon;
using DbHelper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHelper.Repository
{
    public class SMSRepository
    {
        private readonly DapperFactory _dapperFactory;

        public SMSRepository(DapperFactory dapperFactory)
        {
            _dapperFactory = dapperFactory;
        }

        public async Task<ReturnResult> SaveSMSLog(SMSModel model)
        {
            using (var connection = _dapperFactory.GetConnection())
            {
                connection.Open();
                string strSql = " insert into djqm.UserSMS(phoneNumber,smsCode,devManufacturer,devModel,devId,appVersion) values(@phoneNumber,@smsCode,@devManufacturer,@devModel,@devId,@appVersion) ";
                int iReturn = await connection.ExecuteAsync(strSql, model).ConfigureAwait(false);
                return new ReturnResult()
                {
                    successed = true,
                    msg = ""
                };
            }
        }
    }
}
