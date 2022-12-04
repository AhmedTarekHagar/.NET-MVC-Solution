using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using System;

namespace PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IActionResult Index()
        {
            var employees = _employeeRepository.GetAll();
            return View(employees);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeRepository.Add(employee);
                TempData["Message"] = "Employee Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id == null)
                return NotFound();
            var employee = _employeeRepository.Get(id.Value);
            if (employee == null)
                return NotFound();

            return View(ViewName, employee);
        }

        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee employee)
        {
            if (id != employee.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _employeeRepository.Update(employee);
                    TempData["Message"] = "Employee Updated Successfully";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(employee);
                }
            }
            return View(employee);
        }

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id, Employee employee)
        {
            if (id != employee.Id)
                return BadRequest();
            try
            {
                _employeeRepository.Delete(employee);
                TempData["Message"] = "Employee Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(employee);
            }
        }
    }
}
