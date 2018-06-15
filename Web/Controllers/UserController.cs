using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Utils;
using Web.Models.Users;
using Web.Services;

namespace Web.Controllers
{
    public class UserController : BaseController
    {
        UserService _userService;
        public UserController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _userService = new UserService(unitOfWork);
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetUserInfo(string userId)
        {
            var model = _userService.GetUserDetail(userId);
            return Ok(model);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody]CreateUserModel model)
        {
            var userId = ObjectId.GenerateNewStringId();
            var result = _userService.CreateUser(userId,model);
            return Ok(new { Result = result.IsSucess, Msg = result.Message });
        }
        [HttpGet]
        public IActionResult ChangeUserInfo([FromBody]ChangeUserInfoModel model)
        {
            var result = _userService.ChangeUserInfo(model);
            return Ok(new { Result = result.Item1, Msg = result.Item2 });
        }
        [HttpGet]
        public IActionResult GetUserList(int pageIndex=1,int pageSize = 20)
        {
            var model = _userService.GetUserList(pageIndex, pageSize);
            return Ok(model);
        }
        [HttpGet]
        public IActionResult Search(string keyword, int pageIndex = 1, int pageSize = 20)
        {
            UserListModel model;
            if (string.IsNullOrEmpty(keyword))
            {
                model = _userService.GetUserList(pageIndex, pageSize);
            }
            else
            {
                model = _userService.SearchByUserName(keyword,pageIndex, pageSize);
            }
            return Ok(model);
        }

    }
}