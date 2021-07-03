using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace thcp.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [StringLength(30, ErrorMessage = "No debe mas de 30 caracteres")]
        [MinLength(2, ErrorMessage = "Debe tener al menos 2 caracteres")]
        [Display(Name = "Nombres")]
        public string FirstName { get; set; }
        [StringLength(30, ErrorMessage = "No debe mas de 30 caracteres")]
        [MinLength(2, ErrorMessage = "Debe tener al menos 2 caracteres")]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }
        [StringLength(13, ErrorMessage = "No debe mas de 13 caracteres")]
        [MinLength(13, ErrorMessage = "Debe tener al menos 13 caracteres")]
        [Display(Name = "Identidad")]
        public string Indentyfy { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime Birthday { get; set; }
        
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString ="{0:C0}")]
        //se hace porque el dato decimal es diferente de C#
        [Column(TypeName="decimal(18,2)")]
        [Display(Name = "Salario")]

        public decimal Salary { get; set; }
        //relacion con la tabla de departamento
        //nuevo modelo
        //cada vez que se agrege a un empleado se debe colocar en el puesto
        public int PositionId { get; set; }

        [ForeignKey("DepartmetId")]
        public Department Department { get; set; }
        public Position Position { get; set; }

        //relacion de uno a uno

        //public IEnumerable<Department> Departments { get; set; }
        //public IEnumerable<Position> Positions { get; set; }
    }
}
