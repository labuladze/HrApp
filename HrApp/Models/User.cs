using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HrApp.Models
{
    public class User
    {   
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string Lastname { get; set; }

        [DataType(DataType.Date)]
        [Display(Name ="Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        public string Gender { get; set; }

        [RegularExpression("[0-9]*$", ErrorMessage = "Please Enter Only Digits")]
        [StringLength(11, MinimumLength = 11,ErrorMessage ="Length should be 11")]
        [Display(Name="Id Number")]
        public string IdNumber { get; set; }

        [Required]
        [EmailAddress]
        [Remote(action: "VerifyEmail", controller:"Home" )]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [NotMapped]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords does't match")]
        public string RePassword { get; set; }
    }
}
