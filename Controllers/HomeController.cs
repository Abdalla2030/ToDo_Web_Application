using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ToDo_Web_App.Models;

namespace ToDo_Web_App.Controllers
{
    public class HomeController : Controller
    {

        private ToDoContext context;

        public HomeController(ToDoContext ctx) => this.context = ctx; 


        public IActionResult Index(string id)
        {
            var filters = new Filters(id);
            ViewBag.Filters = filters;

            ViewBag.categories = context.categories.ToList();
            ViewBag.statuses = context.statuses.ToList();
            ViewBag.dueFilters = Filters.dueFilterValues;

            IQueryable<ToDo> query = context.ToDos
                .Include(t => t.category)
                .Include(t => t.status);

            if (filters.HasCategory)
            {
                query = query.Where(t => t.categoryID == filters.categoryID);
            }

            if (filters.HasStatus)
            {
                query = query.Where(t => t.statusID == filters.statusID);
            }

            if (filters.HasDue)
            {
                var today = DateTime.Today;
                if (filters.isPast)
                {
                    query = query.Where(t => t.dueDate < today);
                }else if (filters.isFuture)
                {
                    query = query.Where(t => t.dueDate > today);
                }
                else if (filters.isToday)
                {
                    query = query.Where(t => t.dueDate == today);
                }
            }

            var tasks = query.OrderBy(t => t.dueDate).ToList(); 


            return View(tasks);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.categories = context.categories.ToList();
            ViewBag.statuses = context.statuses.ToList();

            var task = new ToDo
            {
                statusID = "open",
            };

            return View(task); 
        }

        [HttpPost]
        public IActionResult Add(ToDo task)
        {
            if (ModelState.IsValid)
            {
                context.ToDos.Add(task);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.categories = context.categories.ToList();
                ViewBag.statuses = context.statuses.ToList();
                return View(task); 
            }
        }

        [HttpPost]
        public IActionResult Filter(string[] filter)
        {
            string id = string.Join('-', filter);
            return RedirectToAction("Index", new { ID = id });
        }

        [HttpPost]
        public IActionResult markComplete([FromRoute] string id,ToDo selected)
        {
            selected = context.ToDos.Find(selected.ID);
            if (selected != null)
            {
                selected.statusID = "closed";
                context.SaveChanges();
            }
            return RedirectToAction("Index", new { ID = id });
        }

        [HttpPost]
        public IActionResult deleteComplete(string id)
        {
            var toDelete = context.ToDos.Where(t => t.statusID == "closed").ToList();

            foreach(var task in toDelete)
            {
                context.ToDos.Remove(task);
            }
            context.SaveChanges();
            return RedirectToAction("Index", new { ID = id });
         }

        
    }
}
