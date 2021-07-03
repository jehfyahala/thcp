using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using thcp.Common;
using thcp.Data;
using thcp.Models;

namespace thcp.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        //codigo añadido
        private readonly int RecordsPerPage = 10;
        private Pagination<Employee> PaginationEmployees;
        //

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }
        //route
        [Route("/Employee")]
        [Route("/Employee/{search}")]
        [Route("/Employee/{search}/{page}")]
        //fin
        // GET: Employees
        public async Task<IActionResult> Index(string search, int page = 1)
        {
            int totalRecords = 0;
            if (search == null)
            {
                search = "";
            }
            //obtener registros totales
            totalRecords = await _context.Employee.CountAsync(
                e=>e.FirstName.Contains(search));
            //obtener datos
            var employees = await _context.Employee
                .Where(e=>e.FirstName.Contains(search)).ToListAsync();
            //paginar
            var employeesResult = employees.OrderBy(o => o.FirstName)
                .Skip((page - 1) * RecordsPerPage)
                .Take(RecordsPerPage);
            //calculo de paginas
            var totalPages = (int)Math.Ceiling((double)totalRecords / RecordsPerPage);
            //instanciar paginacion
            PaginationEmployees = new Pagination<Employee>()
            {
                RecordPerPage=this.RecordsPerPage,
                TotalRecords=totalRecords,
                TotalPage=totalPages,
                CurrentPage=page,
                Search=search,
                Result=employeesResult
            };
            return View(PaginationEmployees);
            //return View(await _context.Employee
            //    .Where(e=>e.FirstName.Contains(search))
            //    .ToListAsync());
        }


        //return View(await _context.Employee.ToListAsync());
        //copia de codigo por si las moscas
        //return View(await _context.Employee
        //.Where(e=>e.FirstName.Contains(search))
        //.ToListAsync());

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }
            lista();
            return View(employee);
        }
        private void lista()
        {
            ViewData["PositionId"] = new SelectList(
                _context.Position, "PositionId", "Description"
            );
        }
        // GET: Employees/Create
        public IActionResult Create()
        {
            lista();
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            lista();
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Bind("EmployeeId,FirtsName,LastName,Indentity,BirthDay,Salary")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }
            lista();
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.EmployeeId == id);
        }
    }
}