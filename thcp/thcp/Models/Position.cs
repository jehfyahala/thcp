using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace thcp.Models
{
    public class Position
    {
        [Key]
        public int PositionId { get; set; }
        [StringLength(70, ErrorMessage = "No debe mas de 70 caracteres")]
        [MinLength(3, ErrorMessage = "Debe tener al menos 3 caracteres")]
        [Display(Name = "Puesto")]
        public string Description { get; set; }
        //declaracion por eso se usa el mismo
        //crear llave foranea y depto id

        public int DepartmetId { get; set; }

        [ForeignKey("DepartmetId")]
        public Department Department { get; set; }

        //relacion de uno a muchos con puesto y empleados
        
        public IEnumerable<Employee> Employees { get; set; }

    }
}
