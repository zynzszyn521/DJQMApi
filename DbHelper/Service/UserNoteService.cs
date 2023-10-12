using DbHelper.Model;
using DbHelper.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHelper.Service
{
    public class UserNoteService
    {
        private readonly UserNoteRepository _userNoteRepository;

        public UserNoteService(UserNoteRepository userNoteRepository)
        {
            _userNoteRepository = userNoteRepository;
        }

        public Task<ReturnResult> SaveUserNote(UserNoteModel model)
        {
            return _userNoteRepository.SaveUserNote(model);
        }
        public Task<dynamic> GetUserNote(string userCode,string appCode)
        {
            return _userNoteRepository.GetUserNote(userCode, appCode);
        }
        public Task<ReturnResult> DeleteUserNote(UserNoteModel model)
        {
            return _userNoteRepository.DeleteUserNote(model);
        }
    }
}
