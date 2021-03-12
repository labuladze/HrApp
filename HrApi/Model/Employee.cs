using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HrApi.Model
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Lastname { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Gender { get; set; }

        [StringLength(11, MinimumLength = 11)]
        public string IdNumber { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string Status { get; set; }

        public DateTime? Fierd { get; set; }

        [StringLength(9, MinimumLength = 9)]
        public string PhoneNumber { get; set; }

    }
}
