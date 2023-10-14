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
    public class UserNoteRepository
    {
        private readonly DapperFactory _dapperFactory;

        public UserNoteRepository(DapperFactory dapperFactory)
        {
            _dapperFactory = dapperFactory;
        }

        public async Task<ReturnResult> SaveUserNote(UserNoteModel model)
        {
            using (var connection = _dapperFactory.GetConnection())
            {
                string strGuid = Guid.NewGuid().ToString();
                model.noteId = strGuid;
                string strSql = string.Empty;
                int iReturn = 0;
                connection.Open();
                strSql = " update djqm.UserNote set title=@title,updateTime=CURRENT_TIMESTAMP where userCode=@userCode and appCode=@appCode and noteDate=@noteDate and noteSex=@noteSex ";
                iReturn = await connection.ExecuteAsync(strSql, model).ConfigureAwait(false);
                if (iReturn == 0)
                {
                    strSql = " insert into djqm.UserNote(noteId,userCode,appCode,title,noteDate,noteSex) values(@noteId,@userCode,@appCode,@title,@noteDate,@noteSex) ";
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
        public async Task<dynamic> GetUserNote(string userCode, string appCode)
        {
            using (var connection = _dapperFactory.GetConnection())
            {
                connection.Open();
                string strSql = " select * from djqm.UserNote where userCode = @userCode and appCode = @appCode ";
                return await connection.QueryAsync<UserNoteModel>(strSql, new { userCode = userCode, appCode = appCode }).ConfigureAwait(false);
            }
        }
        public async Task<ReturnResult> DeleteUserNote(UserNoteModel model)
        {
            int iReturn = 0;
            using (var connection = _dapperFactory.GetConnection())
            {
                connection.Open();
                string strSql = " delete from djqm.UserNote where noteId=@noteId ";
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
}
