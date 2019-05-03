namespace Cet37Market.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class ChangePasswordViewModel
    {
        [Required]
        [Display(Name = "Current Password")]
        public string OldPassword { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string NewPassword { get; set; }

        [Compare("NewPassword")]
        [Display(Name = "Confirm Password")]
        [Required]
        public string Confirm { get; set; }


    }
}
