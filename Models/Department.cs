using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneCRM.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Display(Name = "Employee Department")]
        public string Name { get; set; }

        // Navigation Poperty
        public ICollection<Employee> Employees { get; set; }
    }
}
