using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneCRM.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Business Name")]
        [Required]
        [StringLength(30, ErrorMessage = "Please enter the business name using 30 characters or less.")]
        public string BusinessName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Please enter the address using 50 characters or less.")]
        public string Address { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Please enter the city using 30 characters or less.")]
        public string City { get; set; }

        [Required]
        [StringLength(2, ErrorMessage = "Please enter the state using 2 characters.")]
        public string State { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Zipcode has a maximum length of 10 numbers.")]
        public string Zip { get; set; }

        [Display(Name = "Business Phone")]
        [StringLength(20, ErrorMessage = "Plesae enter the phone in 20 characters or less.")]
        public string BusPhone { get; set; }

        [Display(Name = "Primary Contact")]
        [Required]
        [StringLength(30, ErrorMessage = "Please enter the contact name using 30 characters or less.")]
        public string PrimaryContactName { get; set; }

        [Display(Name = "Contact Phone")]
        [StringLength(20, ErrorMessage = "Plesae enter the phone in 20 characters or less.")]
        public string ContactPhone { get; set; }

        [Display(Name = "Contact Email")]
        [StringLength(50, ErrorMessage = "Plesae limit email entry to 50 characters or less.")]
        public string ContactEmail { get; set; }

        public string CustomerNotes { get; set; }

        // Customer type is defined by different levels of sales
        // Foreign Key
        public int CustomerTypeID { get; set; }

        // Navigation Property
        public CustomerType CustomerType { get; set; }

        // Each customer is assigned to a sales employee
        // Foreign Key
        public int EmployeeId { get; set; }

        // Navigation Property
        public Employee AssignedEmployee { get; set; }


    }
}
