using AutoMapper;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using PL.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var employees = _employeeRepository.GetAll();
            var mappedEmployees = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(mappedEmployees);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
                var mappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                _employeeRepository.Add(mappedEmployee);
                TempData["Message"] = "Employee Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(employeeVM);
        }

        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id == null)
                return NotFound();
            var employee = _employeeRepository.Get(id.Value);
            if (employee == null)
                return NotFound();
            var mappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(employee);

            return View(ViewName, mappedEmployee);
        }

        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var mappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    _employeeRepository.Update(mappedEmployee);
                    TempData["Message"] = "Employee Updated Successfully";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(employeeVM);
                }
            }
            return View(employeeVM);
        }

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            try
            {
                var mappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                _employeeRepository.Delete(mappedEmployee);
                TempData["Message"] = "Employee Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(employeeVM);
            }
        }
    }
}
