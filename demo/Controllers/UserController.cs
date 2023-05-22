using demo.Models;
using demo.Services;
using Microsoft.AspNetCore.Mvc;

namespace demo.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult UserList()
        {
            MainModel model = new MainModel();

            model.UsersList = _userService.GetUserList().ToList();
            return View(model);
        }
        public IActionResult UserAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddUser(User user)
        {
            int i = _userService.SaveUser(user);
            if (i > 0)
            {
                return RedirectToAction("UserList");
            }
            else
            {
                return View();
            }
        }
    }
}
