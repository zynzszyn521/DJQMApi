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
    public class APPRepository
    {
        private readonly DapperFactory _dapperFactory;

        public APPRepository(DapperFactory dapperFactory)
        {
            _dapperFactory = dapperFactory;
        }

        public async Task<dynamic> GetAppServerVersion(string appName)
        {
            using (var connection = _dapperFactory.GetConnection())
            {
                connection.Open();
                string strSql = " select * from djqm.AppVersion where appName=@appName and active=1 ";
                return await connection.QueryAsync<APPModel>(strSql, new { appName = appName }).ConfigureAwait(false);
            }
        }
    }
}
