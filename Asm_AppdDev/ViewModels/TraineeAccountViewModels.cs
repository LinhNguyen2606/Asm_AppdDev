using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Asm_AppdDev.Models;

namespace Asm_AppdDev.ViewModels
{
    public class TraineeAccountViewModels
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Age")]
        public int Age { get; set; }
        [Display(Name = "Birthday")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Display(Name = "Education")]
        public string Education { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}