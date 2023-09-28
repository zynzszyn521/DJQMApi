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
    public class UserRepository
    {
        private readonly DapperFactory _dapperFactory;

        public UserRepository(DapperFactory dapperFactory)
        {
            _dapperFactory = dapperFactory;
        }

        public async Task<ReturnResult> SaveUser(UserModel model)
        {
            using (var connection = _dapperFactory.GetConnection())
            {
                connection.Open();
                string strSql = " insert into djqm.User(userCode,userName,email,password,tel) values(@userCode,@userName,@email,@password,@tel) ";
                int iReturn = await connection.ExecuteAsync(strSql, model).ConfigureAwait(false);
                return new ReturnResult()
                {
                    successed = true,
                    msg = "添加成功"
                };
            }
        }
        public async Task<dynamic> GetUserById(string userCode)
        {
            using (var connection = _dapperFactory.GetConnection())
            {
                connection.Open();
                string strSql = " select * from djqm.User where userCode = @userCode ";
                return await connection.QueryAsync<UserModel>(strSql, new { userCode = userCode }).ConfigureAwait(false);
            }
        }
    }
}
