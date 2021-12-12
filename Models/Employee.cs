using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneCRM.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Number")]
        public int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Please enter the employee name using 30 characters or less.")]
        public string Name { get; set; }

        // Each employee is assigned to a department
        // Foreign Key
        public int DepartmentId { get; set; }
        // Navigation Property
        public Department Department { get; set; }

        // Each employee has a collection of assigned customers
        // Navigation property
        public ICollection<Customer> Customers { get; set; }

        // Each Eemployee has a collection of assigned leads
        // Navigation property
        public ICollection<BusinessLeads> BusinessLeads { get; set; }
    }
}
