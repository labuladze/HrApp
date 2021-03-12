using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HrApp.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name ="Last Name")]
        public string Lastname { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        public string Gender { get; set; }

        [RegularExpression("[0-9]*$", ErrorMessage = "Please Enter Only Digits")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Length should be 11")]
        [Display(Name = "Id Number")]
        [Required]
        [Remote(action: "VerifyIdNumber", controller: "Employee", AdditionalFields = "initialProductCode")]
        public string IdNumber { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string Status { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Fierd { get; set; }

        [RegularExpression("[0-9]*$", ErrorMessage = "Please Enter Only Digits")]
        [StringLength(9, MinimumLength = 9, ErrorMessage ="Length should be 9")]
        [Required]
        public string PhoneNumber { get; set; }

    }
}
