using HrApp.Data;
using HrApp.Models;
using HrApp.Validation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrApp.Controllers
{   
    [ValidateSession]
    public class EmployeeController : Controller
    {
        private readonly IData _data;

        public EmployeeController(IData data)
        {
            _data = data;
        }

        //Get employee/create
        public IActionResult Create()
        {
            return View();
        }

        //Post employee/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee empl)
        {
            if (ModelState.IsValid)
            {
                
                var res = _data.CreateEmpl(empl);
                if (res)
                {
                    TempData["Added"] = "Added";
                    return RedirectToAction("Profile", "Home");
                }
            }
            return View();
        }

        //Get: employee/edit/id
        public IActionResult Edit(int id)
        {
            var res = _data.GetById(id);
            if (res != null)
            {
                return View(res);
            }
            return NotFound();
        }

        //Post: employee/edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee empl)
        {
            if (ModelState.IsValid)
            {
                var res = _data.UpdateEmpl(empl.Id, empl);
                if (res)
                {
                    TempData["Updated"] = "Updated";
                    return RedirectToAction("Profile", "Home");
                }
            }
            return View();
        }

        //Post: employee/delete/id
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var res = _data.DeleteEmpl(id);
            if (res)
            {               
                return RedirectToAction("Profile", "Home");
            }
            return View();
        }

        #region IdNumber Validation
        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyIdNumber(string IdNumber, string initialProductCode)
        {
            var res = _data.GetAll().FirstOrDefault(x => x.IdNumber == IdNumber);
            if (IdNumber == initialProductCode || res == null)
            {
                return Json(true);
            }
            return Json($"The User with {IdNumber} Id already exists");
        }
        #endregion
    }
}
