using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Grundlagen.Models;
using Web_Grundlagen.Models.Db;


namespace Web_Grundlagen.Controllers
{
    public class UserController : Controller
    {

        private DBContext _context = new DBContext();
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Login(User user)
        {
            if(ModelState.IsValid)
            {
                List<User> allusers = new List<User>();
                allusers = this._context.User.ToList();

                foreach (User u in allusers)
                {
                    if (u.Email == user.Email)
                    {
                        var passwordHasher = new PasswordHasher<IdentityUser>();
                        if (passwordHasher.VerifyHashedPassword(null, u.Password, user.Password) == PasswordVerificationResult.Success)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return View("Message", new Message()
                            {
                                title = "Login fehlgeschlagen",
                                messagetext = "Falsches Passwort",
                            });
                        }
                    }
                }
                return View("Message", new Message()
                {
                    title = "Login fehlgeschlagen",
                    messagetext = "Kein User mit dieser Email registriert",
                });
            }
            return View();

        }
        public IActionResult Registration()
        {
            return View( new User() { Birthdate = DateTime.Today });
        }
        //User user ... sind die Daten des Formulars 
        [HttpPost]
        public async Task<IActionResult> Registration(User user)
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
            if (user.Password != user.PasswordRetype || user.Password == null)
            {
                ModelState.AddModelError("Passwort", "Passwort stimmt nicht überein oder ist leer");
            }
            else
            {
                var passwordHasher = new PasswordHasher<IdentityUser>();
                user.Password = passwordHasher.HashPassword(null, user.Password);
            }

            //OK -> in db abspeichern + Erfolg melden
            if(ModelState.IsValid)
            {
                //In DB abspeichern (ORM) - bitte Rückgabewert von SaveChangesAsync() verwenden und dan entsprechende Meldungen ausgeben

                this._context.User.Add(user);
                await this._context.SaveChangesAsync();

                /// TODO: DB-Teil
                ///     ORM (EF-Core) verwendet
                ///         C1. 3 Pakete installieren
                ///         C2. DbContext Klasse programmieren
                ///         C3. FluentAPI bzw. Annotationen 
                ///             Email -> unique
                ///             PasswordRetype sollte nicht in der DB aufscheinen)
                ///         C4. Migrations (2 Befehle)
                ///         C5. Erzeugte DB überprüfen
                ///         C6. DB-Teil in der registration eibauen
                ///             Passwort sollte gehashed werden (PasswordHascher)
                ///         7. Login
                ///             View erzeugen
                ///             Methode programmieren Login()
                ///             überprüfen ob User mit Passwort in der DB existiert + Erfolgs- bzw. Fehlermeldung
                ///         

                //View mit Erfolgsmeldung anzeigen
                return View("Message", new Message()
                {
                    title = "Registrierung erfolgreich",
                    messagetext = "Sie wurden erfolgreich registriert",
                });
            }


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
            List<User> allusers = new List<User>();
            allusers = this._context.User.ToList();
            //Daten einer Userliste vom Kontroller an die View übergeben
            return View(allusers);
        }
        public IActionResult deleteuser() {
            return View("Message", new Message()
            {
                title = "Login fehlgeschlagen",
                messagetext = "Falsches Passwort",
            });
            ///TODO USER LÖSCHEN PROGRAMMIEREN DES WAS DA ISCH ISCH LEI TEMPORÄR ZUM TESTEN
        }
        public IActionResult changeuser() {
            return View("Message", new Message()
            {
                title = "Login fehlgeschlagen",
                messagetext = "Falsches Passwort",
            });
            ///TODO USER ÄNDERN PROGRAMMIEREN DES WAS DA ISCH ISCH LEI TEMPORÄR ZUM TESTEN
        }
    }
}
