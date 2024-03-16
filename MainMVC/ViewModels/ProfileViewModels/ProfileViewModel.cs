using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MainMVC.ViewModels.ProfileViewModels
{
    public class ProfileViewModel
    {
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Role")]
        public string Role { get; set; }

        [Display(Name = "Enabled")]
        public bool Enabled { get; set; }
    }
}
