using System.ComponentModel.DataAnnotations.Schema;

namespace Web_Grundlagen.Models
{
    public class User
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; }
        [NotMapped]
        public string PasswordRetype { get; set; } = string.Empty;
        public string Email { get; set; }
        public DateTime Birthdate { get; set; } = DateTime.Today;
    }
}
