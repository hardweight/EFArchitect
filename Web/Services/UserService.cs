using DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Web.Models.Users;

namespace Web.Services
{
    public class UserService
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<User> _userRepository;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepository = unitOfWork.GetRepository<User>();
        }

        public UserDetailModel GetUserDetail(string userId)
        {
            var user = _userRepository.GetFirstOrDefault(predicate: o => o.Id == userId);
            if (user != null)
            {
                return new UserDetailModel
                {
                    Id = user.Id,
                    UserName = user.UserName
                };
            }
            return null;
        }

        public Tuple<bool, string> CreateUser(string userId,CreateUserModel model)
        {
            var user = new User
            {
                Id = userId,
                UserName = model.UserName
            };
            _userRepository.Insert(user);
            _unitOfWork.SaveChanges();
            return new Tuple<bool, string>(true, "保存成功");
        }

        public Tuple<bool, string> RemoveUser(string userId)
        {
            if (_userRepository.Count(o => o.Id == userId) > 0)
            {
                _userRepository.Delete(userId);
                _unitOfWork.SaveChanges();
            }
            return new Tuple<bool, string>(true, "保存成功");
        }

        public Tuple<bool, string> ChangeUserInfo(ChangeUserInfoModel model)
        {
            var user = _userRepository.Find(model.Id);
            if (user == null)
            {
                return new Tuple<bool, string>(false, "用户不存在或者已经被删除");
            }
            else
            {
                user.UserName = model.UserName;
                _unitOfWork.SaveChanges();
                return new Tuple<bool, string>(true, "保存成功");
            }
        }

        public UserListModel GetUserList(int pageIndex = 1, int pageSize = 20)
        {
            return GetUserListByPredicate(null, pageIndex, pageSize);
        }

        public UserListModel SearchByUserName(string keword, int pageIndex = 1, int pageSize = 20)
        {
            return GetUserListByPredicate(o => o.UserName.Contains(keword), pageIndex, pageSize);
        }

        private UserListModel GetUserListByPredicate(Expression<Func<User, bool>> predicate, int pageIndex, int pageSize)
        {
            var data = _userRepository.GetPagedList(o =>
                new UserDataModel
                {
                    Id = o.Id,
                    UserName = o.UserName
                },
                predicate: predicate,
                orderBy: o => o.OrderByDescending(a => a.Id),
                pageIndex: pageIndex,
                pageSize: pageSize
            );
            if (data != null && data.TotalCount > 0)
            {
                return new UserListModel
                {
                    PageIndex = data.PageIndex,
                    TotalCount = data.TotalCount,
                    Items = data.Items,
                    TotalPage = data.TotalPages
                };
            }
            return null;
        }
    }
}
