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
    public class OrderRepository
    {
        private readonly DapperFactory _dapperFactory;

        public OrderRepository(DapperFactory dapperFactory)
        {
            _dapperFactory = dapperFactory;
        }

        public async Task<ReturnResult> SaveOrder(OrderModel model)
        {
            using (var connection = _dapperFactory.GetConnection())
            {
                connection.Open();
                string strSql = " insert into djqm.UserOrder(orderId,userCode,orderType,productId,price,payType) values(@orderId,@userCode,@orderType,@productId,@price,@payType) ";
                int iReturn = await connection.ExecuteAsync(strSql, model).ConfigureAwait(false);
                return new ReturnResult()
                {
                    successed = true,
                    msg = "添加成功"
                };
            }
        }
        public async Task<dynamic> GetOrderList(string userCode)
        {
            using (var connection = _dapperFactory.GetConnection())
            {
                connection.Open();
                string strSql = " select * from djqm.UserOrder where userCode = @userCode ";
                return await connection.QueryAsync<OrderModel>(strSql, new { userCode = userCode }).ConfigureAwait(false);
            }
        }
    }
}
