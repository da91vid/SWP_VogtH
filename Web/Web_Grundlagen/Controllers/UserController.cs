using Microsoft.AspNetCore.Mvc;
using Web_Grundlagen.Models;

namespace Web_Grundlagen.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Registration()
        {
            return View();
        }
        public IActionResult ShowOneUser()
        {
            //Daten eines Users vom Kontroller an die View übergeben
            //Die Userdaten kommen normal aus einer DB
            User u = new User()
            {
                Id = 5,
                Name = "Red Head Johnny's little Pommes",
                Birthdate = new DateTime(2004, 02, 04)
            };

            //User an die View übergeben
            return View(u);
        }
        public IActionResult ShowMultipleUser()
        {
            //Daten einer Userliste vom Kontroller an die View übergeben
            return View();
        }
    }
}
