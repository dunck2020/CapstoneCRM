using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneCRM.Models
{
    public class CustomerType
    {
        // Customers are assigned a type or class dependent on manager and sales
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Display(Name = "Customer Class")]
        public string Name { get; set; }

        // Each type will have a collection of customers
        // Navigation Poperty
        public ICollection<Customer> Customers { get; set; }
    }
}
