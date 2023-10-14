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
                string strSql = string.Empty;
                int iReturn = 0;
                connection.Open();
                strSql = " update djqm.User set lastTime=CURRENT_TIMESTAMP where userCode=@userCode ";
                iReturn = await connection.ExecuteAsync(strSql, model).ConfigureAwait(false);
                if (iReturn == 0)
                {
                    strSql = " insert into djqm.User(userCode,userName,email,password,phoneNumber) values(@userCode,@userName,@email,@password,@phoneNumber) ";
                    iReturn = await connection.ExecuteAsync(strSql, model).ConfigureAwait(false);
                }
                return new ReturnResult()
                {
                    successed = true,
                    msg = "添加成功"
                };
            }
        }
        public async Task<ReturnResult> UpdateVipUser(UserModel model)
        {
            using (var connection = _dapperFactory.GetConnection())
            {
                string strSql = string.Empty;
                int iReturn = 0;
                connection.Open();
                strSql = " update djqm.User set vipFlag=@vipFlag,expirationTime=@expirationTime where userCode=@userCode ";
                iReturn = await connection.ExecuteAsync(strSql, model).ConfigureAwait(false);
                return new ReturnResult()
                {
                    successed = true,
                    msg = ""
                };
            }
        }
        public async Task<ReturnResult> SaveUserVipH(UserVipHModel model)
        {
            using (var connection = _dapperFactory.GetConnection())
            {
                model.recordId = Guid.NewGuid().ToString();
                string strSql = string.Empty;
                int iReturn = 0;
                connection.Open();
                strSql = " insert into djqm.UserVipH(recordId,userCode,vipTypeId,vipTypeName,vipTypePrice) values(@recordId,@userCode,@vipTypeId,@vipTypeName,@vipTypePrice) ";
                iReturn = await connection.ExecuteAsync(strSql, model).ConfigureAwait(false);
                return new ReturnResult()
                {
                    successed = true,
                    msg = ""
                };
            }
        }
        public async Task<IEnumerable<UserModel>> GetUserById(string userCode)
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
