using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using thcp.Data;
using thcp.Models;

namespace thcp.Controllers
{
    public class DepartmentnsController : Controller
    {
        private readonly ApplicationDbContext db;

        public DepartmentnsController(ApplicationDbContext db)
        {
            this.db = db; //variable arriba es igual a db en el metodo constructor applicationdbcontext es un servicio cuando se crea la clase se inicia y se pasa a variable reandonly
        }

        //leer cualquier cosa de la base async hacer cualquier cosa mientras se ejecuta
        //IActionResult es una interfaz parte de entrada
        public async Task<IActionResult> Index()
        {
            //devuelve todos los elementos de la tabla de departamentos
            //Devuelve el parametro para el metodo index
            return View(await db.Departments.ToListAsync());
        }
        //crear por medio de vista un formulario
        public IActionResult Create()
        {
            return View();
        }
        //mostrar el campo descriptivo

        [HttpPost]//metodo atraves de post
        [ValidateAntiForgeryToken]//complementa la validacion en el metodo si es correcto y no venga codigo malicioso
        public async Task<IActionResult> Create(Department department)
        {
            //curarnos en salud
            if (ModelState.IsValid)
            {
                db.Add(department);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }
    }
}
