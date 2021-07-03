using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using thcp.Common;
using thcp.Data;
using thcp.Models;

namespace thcp.Controllers
{
    public class DepartmentnsController : Controller
    {
        //new cod secund partial
        //var
        private readonly int RecordsPerPage = 10;
        private readonly ApplicationDbContext db;

        private Pagination<Department> PaginationDepartments;
        //la busqueda se trabaja sobre el index con el nuevo parametro opcional


        public DepartmentnsController(ApplicationDbContext db)
        {
            this.db = db; //variable arriba es igual a db en el metodo constructor applicationdbcontext es un servicio cuando se crea la clase se inicia y se pasa a variable reandonly
        }

        //leer cualquier cosa de la base async hacer cualquier cosa mientras se ejecuta
        //IActionResult es una interfaz parte de entrada
        //nuevo codigo para buscar
        //parametro opcional debe ir al final
        //revisar este codigo Departmentns
        [Route("/Departments")]
        [Route("/Departments/{search}")]
        [Route("/Departments/{search}/{page}")]
        //fin de codigo a revisar
        public async Task<IActionResult> Index(string search, int page=1)
        {

            //modificacion
            int totalRecords = 0;
            if (search==null)
            {
                //modificacion1

                search = "";
            }
            //obtener registros totales
            totalRecords = await db.Departments.CountAsync(
            //expresion landa
            d=>d.DepartmentName.Contains(search));

            //obtener datos
            var departments = await db.Departments
                .Where(d=>d.DepartmentName.Contains(search)).ToListAsync();

            //paginar
            var departmentsResult = departments.OrderBy(o => o.DepartmentName)
                .Skip((page - 1) * RecordsPerPage)
                .Take(RecordsPerPage);
            //calculo de las paginas
            var totalPages = (int)Math.Ceiling((double)totalRecords / RecordsPerPage);

            //instanciar paginacion

            PaginationDepartments = new Pagination<Department>() 
            {
                RecordPerPage = this.RecordsPerPage,
                TotalRecords=totalRecords,
                TotalPage=totalPages,
                CurrentPage =page,
                Search=search,
                Result=departmentsResult
            };


            //fin paginar
            //devuelve todos los elementos de la tabla de departamentos
            //Devuelve el parametro para el metodo index
            //cambio nuevo definimos una expresion landa para poder hacer las busquedas
            //envio nuevo modelo
            return View(PaginationDepartments);
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
