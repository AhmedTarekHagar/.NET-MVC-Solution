using BLL.Interfaces;
using BLL.Repositories;
using DAL.Entities;
using PL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System;
using AutoMapper;
using System.Collections;
using System.Collections.Generic;

namespace PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var departments = _unitOfWork.Repository<Department>().GetAll();
            var mappedDepartments = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);
            return View(mappedDepartments);
        }
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DepartmentViewModel departmentVM)
        {
            if (ModelState.IsValid)
            {
                var mappedDepartment = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                _unitOfWork.Repository<Department>().Add(mappedDepartment);
                TempData["Message"] = "Department Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(departmentVM);
        }

        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if(id == null)
                return NotFound();
            var department = _unitOfWork.Repository<Department>().Get(id.Value);
            if (department == null)
                return NotFound();
            var mappedDepartment = _mapper.Map<Department, DepartmentViewModel>(department);

            return View(ViewName ,mappedDepartment);
        }

        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id, DepartmentViewModel departmentVM)
        {
            if(id != departmentVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var mappedDepartment = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                    _unitOfWork.Repository<Department>().Update(mappedDepartment);
                    TempData["Message"] = "Department Updated Successfully";
                    return RedirectToAction(nameof(Index));
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(departmentVM);
                }
            }
            return View(departmentVM);
        }

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id, DepartmentViewModel departmentVM)
        {
            if (id != departmentVM.Id)
                return BadRequest();
            try
            {
                var mappedDepartment = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                _unitOfWork.Repository<Department>().Delete(mappedDepartment);
                TempData["Message"] = "Department Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(departmentVM);
            }
        }
    }
}
