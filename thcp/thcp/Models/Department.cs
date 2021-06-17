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
        //data notation
        [Required(ErrorMessage ="El nombre es obligatorio")]
        [Display(Name = "Nombre")]
        [StringLength(70,ErrorMessage ="No debe mas de 70 caracteres")]
        [MinLength(3,ErrorMessage ="Debe tener al menos 3 caracteres")]
        public string DepartmentName { get; set; }
        
    }
}
