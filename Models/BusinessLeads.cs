using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneCRM.Models
{
    public class BusinessLeads
    {
        // Class designed to accept leads with minimum information from user.  Only required attribute
        // us lead business name
        [Key]
        public int Id { get; set; }

        [Display(Name = "Business Name")]
        [Required]
        [StringLength(30, ErrorMessage = "Please enter the business name using 30 characters or less.")]
        public string BusinessName { get; set; }

        [StringLength(50, ErrorMessage = "Please enter the address using 50 characters or less.")]
        public string Address { get; set; }

        [StringLength(30, ErrorMessage = "Please enter the city using 30 characters or less.")]
        public string City { get; set; }

        [StringLength(2, ErrorMessage = "Please enter the state using 2 characters.")]
        public string State { get; set; }

        [Display(Name = "Contact")]
        [StringLength(30, ErrorMessage = "Please enter the contact name using 30 characters or less.")]
        public string Contact { get; set; }

        [Display(Name = "Phone")]
        [StringLength(20, ErrorMessage = "Please enter the phone number in 20 characters or less.")]
        public string Phone { get; set; }


        // Each lead is assigned to an employee at creation
        // Foreign Key
        public int EmployeeId { get; set; }

        // Navigation Property
        public Employee AssignedEmployee { get; set; }
    }
}
