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
            return View( new User() { Birthdate = DateTime.Today });
        }
        //User user ... sind die Daten des Formulars 
        [HttpPost]
        public IActionResult Registration(User user)
        {
            //Formular überprüfen (valedieren)
            //Überprüfung der Formulardaten am Server muss immer statt finden
            if (user.Name.Trim().Length < 2)
            {
                ModelState.AddModelError("Name", "Der Name muss mindestens 2 Zeichen lang sein!");
            }
            if (!user.Email.Contains('@'))
            {
                ModelState.AddModelError("Email", "Die Email muss ein @ enthalten!");
            }
            //Birthday ist kein Pflichtfeld und es muss sich in der VErgangenheit befinen
            if (user.Birthdate != DateTime.Today && user.Birthdate > DateTime.Today)
            {
                ModelState.AddModelError("Birthdate", "DU bist zu jung oder zu alt!");
            }
            //OK -> in db abspeichern + Erfolg melden

            //nicht OK -> Formular erneut mit den eingegebenen Daten anzeigen inkl. Fehlermeldungen
            return View(user);
        }
        public IActionResult ShowOneUser()
        {
            //Daten eines Users vom Kontroller an die View übergeben
            //Die Userdaten kommen normal aus einer DB
            User u = new User()
            {
                Id = 5,
                Name = "Red Head Johnny's little Pommes",
                Birthdate = new DateTime(2004, 02, 04),
                Email = "test@gmx.at"
            };

            //User an die View übergeben
            return View(u);
        }
        public IActionResult ShowMultipleUser()
        {

            List<User> users = new List<User>()
            {
                new User()
                {
                    Id = 5,
                    Name = "Red Head Johnny's little Pommes",
                    Birthdate = new DateTime(2004, 02, 04),
                    Email = "simma@gmx.at"
                },
                new User()
                {
                    Id = 6,
                    Name = "Lactose Luca",
                    Birthdate = new DateTime(2003, 02, 04),
                    Email = "lactose@gmx.at"
                },
                new User()
                {
                    Id = 7,
                    Name = "SteffGPT",
                    Birthdate = new DateTime(2002, 02, 04),
                    Email = "chat@gmx.at"
                }
            };
            //Daten einer Userliste vom Kontroller an die View übergeben
            return View(users);
        }
    }
}
