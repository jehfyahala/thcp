using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace thcp.Models
{
    public class Department
    {
        [Key]
        public int DepartmetId { get; set; }
        [Display(Name = "Nombre")]
        public string DepartmentName { get; set; }
        
    }
}
