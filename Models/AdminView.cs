using System.ComponentModel.DataAnnotations;

namespace CarCleanz.Models
{
    public class AdminView
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}