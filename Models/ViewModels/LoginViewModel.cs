using System.ComponentModel.DataAnnotations;

namespace Project_Management.Models.ViewModels
{
    public class LoginViewModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        [Display(Name = "Rmemeber Me")]
        public bool IsRemember { get; set; }
    }
}
