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

        public IActionResult GetUser(string id)
        {
            var model = _userService.GetUserDetail(id);
            return Ok(model);
        }

        [HttpPost]
        public IActionResult CreateUser(CreateUserModel model)
        {
            var result = _userService.CreateUser(model);
            if (result)
                return Ok(new { Result = true });
            else
                return Ok(new { Result = false, Msg = "保存失败" });
        } 

        public IActionResult ChangeUserInfo(ChangeUserInfoModel model)
        {
            var result = _userService.ChangeUserInfo(model);
            if (result.Item1)
            {
                return Ok(new { Result = true });
            }
            else
            {
                return Ok(new { Result = false, Msg = result.Item2 });
            }
        }
    }
}