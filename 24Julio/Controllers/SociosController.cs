using _24Julio.Data;
using _24Julio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _24Julio.Controllers
{
    [Authorize]
    public class SociosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SociosController (ApplicationDbContext context)
        {
            _context = context;

        }
        [Authorize(Roles = "Admin, User")]

        // GET: SociosController
        public ActionResult Index()
        {
            List<Socios> ltssocios = new List<Socios>();
            ltssocios = _context.Socios.ToList();

            return View(ltssocios);
        }
        [Authorize (Roles ="Admin")]
        // GET: SociosController/Details/5
        public ActionResult Details(string id)
        {

            Socios socios = _context.Socios.Where(x => x.Cedula == id).FirstOrDefault();
            return View(socios);
        }
        [Authorize(Roles = "Admin")]
        // GET: SociosController/Create
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        // POST: SociosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Socios socios)
        {
            try
            {
                socios.Estado = 1;
                _context.Add(socios);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(socios);
            }
        }
        [Authorize(Roles = "Admin")]
        // GET: SociosController/Edit/5
        public ActionResult Edit(String id)
        {

            Socios socios = _context.Socios.Where(x => x.Cedula == id).FirstOrDefault();
            return View(socios);
           
        }
        [Authorize(Roles = "Admin")]
        // POST: SociosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(String id, Socios socios)
        {
            if(id != socios.Cedula )
            {

                return RedirectToAction("Index");
            }
            try
            {
                _context.Update(socios);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(socios);
            }
        }
        [Authorize(Roles = "Admin")]
        // GET: SociosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        // POST: SociosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Activar(String id)
        {

            Socios socios = _context.Socios.Where(x => x.Cedula == id).FirstOrDefault();
            socios.Estado = 1;
            _context.Update(socios);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
        [Authorize(Roles = "Admin")]
        public ActionResult Desactivar(String id)
        {

            Socios socios = _context.Socios.Where(x => x.Cedula == id).FirstOrDefault();

            socios.Estado = 0;
            _context.Update(socios);
            _context.SaveChanges();
            return RedirectToAction("Index");
           

        }

    }

}
