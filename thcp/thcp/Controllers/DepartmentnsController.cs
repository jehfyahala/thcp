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
        //nuevo codigo para buscar
        public async Task<IActionResult> Index(string search)
        {
            if (search==null)
            {
                return View(await db.Departments.ToListAsync());
            }
            //devuelve todos los elementos de la tabla de departamentos
            //Devuelve el parametro para el metodo index
            //cambio nuevo definimos una expresion landa para poder hacer las busquedas
            return View(await db.Departments
                .Where(d => d.DepartmentName.Contains(search))
                
                .ToListAsync());
        }
        //crear por medio de vista un formulario

        //vista de departamentos en pocas palabras consultas
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var depart = await db.Departments.FirstOrDefaultAsync(d => d.DepartmetId == id);
            if (depart == null)
            {
                return NotFound();
            }
            return View(depart);
        }


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

        //metodo de editar
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var depart = await db.Departments.FindAsync(id);
            if (depart == null)
            {
                return NotFound();
            }

            return View(depart);
        }

        //guardar modificaciones en la dbf
        [HttpPost]//metodo atraves de post
        [ValidateAntiForgeryToken]//complementa la validacion en el metodo si es correcto y no venga codigo malicioso
        public async Task<IActionResult> Edit(int id, Department depart)
        {
            if (id != depart.DepartmetId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(depart);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(depart);
        }
        //fin editar
        //borrar metodo final
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var depart = await db.Departments.FirstOrDefaultAsync(d => d.DepartmetId == id);
            //buscar en la tabla de departamentos y quedara en depart
            if (depart == null)
            {
                return NotFound();
            }

            return View(depart);
        }
        //inicio borrar
        [HttpPost, ActionName("Delete")]//metodo atraves de post, el metodo hace el llamado
        [ValidateAntiForgeryToken]//complementa la validacion en el metodo si es correcto y no venga codigo malicioso
        public async Task<IActionResult> Delete(int id)
        {
            var depart = await db.Departments.FindAsync(id);
            db.Departments.Remove(depart);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //fin borrar


    }
}
