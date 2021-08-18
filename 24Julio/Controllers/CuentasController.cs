using _24Julio.Data;
using _24Julio.Models;
using _24Julio.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _24Julio.Controllers

{
    [Authorize]
    public class CuentasController : Controller
    {
        
        private readonly ApplicationDbContext _context;

        public CuentasController(ApplicationDbContext context)
        {

            _context = context;

        }
         private void Combox()
        {
            ViewData["CodigoSocio"] = new SelectList(_context.Socios.Select(x => new SocioCuenta
            {
                CedulaSocio = x.Cedula,
                NombreSocio = $"{x.Nombre} {x.Apellido}",
                Estado = x.Estado
            }).Where(x => x.Estado == 1).ToList(), "", "CedulaSocio", "NombreSocio");
           
        }
        [Authorize(Roles = "Admin, Usuario")]
        // GET: CuentasController
        public ActionResult Index()
        {
            List<Cuentas> ltscuentas = new List<Cuentas>();
            ltscuentas = _context.Cuenta.ToList();

            return View(ltscuentas);
        }
        [Authorize(Roles = "Admin, Usuario")]
        // GET: CuentasController/Details/5
        public ActionResult Details(string id)
        {
            Cuentas cuentas = _context.Cuenta.Where(x => x.Numero == id).FirstOrDefault();
            return View(cuentas);
            
        }
        [Authorize(Roles = "Admin")]
        // GET: CuentasController/Create
        public ActionResult Create()
        {
            Combox();
            return View();
        }
        [Authorize(Roles = "Admin")]
        // POST: CuentasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cuentas cuentas)
        {
            try
            {
                cuentas.Estado = 1;
                _context.Add(cuentas);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(cuentas);
            }
        }
        [Authorize(Roles = "Admin")]
        // GET: CuentasController/Edit/5
        public ActionResult Edit(string id)
        {
            Cuentas cuentas = _context.Cuenta.Where(x => x.Numero == id).FirstOrDefault();
            Combox();
            return View(cuentas);
        }
        [Authorize(Roles = "Admin")]
        // POST: CuentasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Cuentas cuentas)
        {
            if(id != cuentas.CodigoSocio)
                {
                return RedirectToAction("Index");

            }

            try
            {

                _context.Update(cuentas);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                Combox();
                return View(cuentas);
            }
        }
        [Authorize(Roles = "Admin")]
        // GET: CuentasController/Delete/5
        public ActionResult Activar(string id)
        {
            Cuentas cuentas = _context.Cuenta.Where(x => x.Numero == id).FirstOrDefault();
            cuentas.Estado = 1;
            _context.Update(cuentas);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Desactivar(string id)
        {
            Cuentas cuentas = _context.Cuenta.Where(x => x.Numero == id).FirstOrDefault();
            cuentas.Estado = 0;
            _context.Update(cuentas);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
