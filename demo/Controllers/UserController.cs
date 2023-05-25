using demo.Models;
using demo.Services;
using Microsoft.AspNetCore.Mvc;
using static demo.Services.UserServices;

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
            var emailExist =_userService.CheckEmailAvailability(user.Email);
            if (emailExist)
            {
                ModelState.AddModelError("email", "Already exsist");
                return View("UserAdd");
            }
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

        public IActionResult listOfState()
        {
            IEnumerable<State> states = _userService.GetStateList();
            return Json(states);
        }
        [HttpGet]
        public IActionResult GetCities(int stateId)
        {
            
            IEnumerable<City> cities = _userService.GetCityList(stateId);

            return Json(cities);
        }

        public IActionResult UserValidation()
        {
            return View();
        }

    }
}
