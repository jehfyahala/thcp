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
    public class PositionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        //codigo añadido
        private readonly int RecordsPerPage = 10;
        private Pagination<Position> PaginationPositions;
        //
        public PositionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //route
        [Route("/Position")]
        [Route("/Position/{search}")]
        [Route("/Position/{search}/{page}")]
        //fin 

        // GET: Positions
        public async Task<IActionResult> Index(string search, int page=1)
        {
            int totalRecords = 0;
            if (search==null)
            {
                search = "";
            }
            //obtener registros totales
            totalRecords = await _context.Position.CountAsync(
                p=>p.Description.Contains(search));
            //obtener datos
            var positions = await _context.Position
                .Where(p => p.Description.Contains(search)).ToListAsync();
            //paginar
            var positionsResult = positions.OrderBy(o => o.Description)
                .Skip((page - 1) * RecordsPerPage)
                .Take(RecordsPerPage);
            //calculo de paginas
            var totalPages = (int)Math.Ceiling((double)totalRecords / RecordsPerPage);
            //instanciar paginacion
            PaginationPositions = new Pagination<Position>()
            {
                RecordPerPage=this.RecordsPerPage,
                TotalRecords=totalRecords,
                TotalPage=totalPages,
                CurrentPage=page,
                Search=search,
                Result=positionsResult
            };
            //if (search==null)
            //{
            //    return View(await _context.Position.ToListAsync()); // codigo original
            //}
            //return View(await _context.Position
            //    .Where(p=> p.Description.Contains(search))
            //    .ToListAsync()
            //    );
            return View(PaginationPositions);
        }

        // GET: Positions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var position = await _context.Position
                .FirstOrDefaultAsync(m => m.PositionId == id);

            //Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Position, Department> includableQueryable = _context.Position.Include(p => p.Department);
            //_context.Position
            //.FirstOrDefaultAsync(m => m.PositionId == id);
            if (position == null)
            {
                return NotFound();
            }
            
            lista();
            
            return View(position);
        }

        // GET: Positions/Create
        public IActionResult Create()
        {
            //nuevo codigo
            lista();
            return View();
        }

        //nuevo procedimiento para evitar codigo repetido
        private void lista()
        {
            ViewData["DepartmetId"] = new SelectList(
                _context.Departments, "DepartmetId", "DepartmentName");
        }
        //fin procedimiento

        // POST: Positions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Position position)
        {
            if (ModelState.IsValid)
            {
                _context.Add(position);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(position);
        }

        // GET: Positions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            lista();
            if (id == null)
            {
                return NotFound();
            }

            var position = await _context.Position.FindAsync(id);
            if (position == null)
            {
                return NotFound();
            }
            return View(position);
        }

        // POST: Positions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Position position)
        {
            if (id != position.PositionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(position);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PositionExists(position.PositionId))
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
            return View(position);
        }

        // GET: Positions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var position = await _context.Position
                .FirstOrDefaultAsync(m => m.PositionId == id);
            if (position == null)
            {
                return NotFound();
            }
            lista();
            return View(position);
        }

        // POST: Positions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var position = await _context.Position.FindAsync(id);
            _context.Position.Remove(position);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PositionExists(int id)
        {
            return _context.Position.Any(e => e.PositionId == id);
        }
    }
}
