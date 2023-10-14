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
    public class VipPriceRepository
    {
        private readonly DapperFactory _dapperFactory;

        public VipPriceRepository(DapperFactory dapperFactory)
        {
            _dapperFactory = dapperFactory;
        }

        public async Task<ReturnResult> SaveVipPrice(VipPriceModel model)
        {
            using (var connection = _dapperFactory.GetConnection())
            {
                string strSql = string.Empty;
                int iReturn = 0;
                connection.Open();
                strSql = " update djqm.VipPrice set updateTime=CURRENT_TIMESTAMP,typePrice=@typePrice where typeId=@typeId ";
                iReturn = await connection.ExecuteAsync(strSql, model).ConfigureAwait(false);
                if (iReturn == 0)
                {
                    strSql = " insert into djqm.VipPrice(typeId,typeName,typePrice,monthLength) values(@typeId,@typeName,@typePrice,@monthLength) ";
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
        public async Task<dynamic> GetVipPrice(string userCode)
        {
            //後續需要可以根據用戶來修改特定價格
            using (var connection = _dapperFactory.GetConnection())
            {
                connection.Open();
                string strSql = " select * from djqm.VipPrice";
                return await connection.QueryAsync<VipPriceModel>(strSql, new { userCode = userCode }).ConfigureAwait(false);
            }
        }
    }
}
