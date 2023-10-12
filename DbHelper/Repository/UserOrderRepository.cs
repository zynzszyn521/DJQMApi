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
    public class UserOrderRepository
    {
        private readonly DapperFactory _dapperFactory;

        public UserOrderRepository(DapperFactory dapperFactory)
        {
            _dapperFactory = dapperFactory;
        }

        public async Task<ReturnResult> SaveUserOrder(UserOrderModel model)
        {
            using (var connection = _dapperFactory.GetConnection())
            {
                string strSql = string.Empty;
                int iReturn = 0;
                connection.Open();
                strSql = " update djqm.UserOrder set createTime=CURRENT_TIMESTAMP where orderId=@orderId ";
                iReturn = await connection.ExecuteAsync(strSql, model).ConfigureAwait(false);
                if (iReturn == 0)
                {
                    strSql = " insert into djqm.UserOrder(orderId,userCode,orderType,productId,price,payType) values(@orderId,@userCode,@orderType,@productId,@price,@payType) ";
                    iReturn = await connection.ExecuteAsync(strSql, model).ConfigureAwait(false);
                }
                if (iReturn > 0)
                {
                    return new ReturnResult()
                    {
                        successed = true,
                        msg = ""
                    };
                }
                else
                {
                    return new ReturnResult()
                    {
                        successed = false,
                        msg = ""
                    };
                }
            }
        }
        public async Task<dynamic> GetUserOrder(string userCode, string orderType,string productId)
        {
            using (var connection = _dapperFactory.GetConnection())
            {
                connection.Open();
                string strSql = " select * from djqm.UserOrder where userCode = @userCode and orderType = @orderType and productId = @productId ";
                return await connection.QueryAsync<UserOrderModel>(strSql, new { userCode = userCode, orderType = orderType, productId = productId }).ConfigureAwait(false);
            }
        }
    }
}
