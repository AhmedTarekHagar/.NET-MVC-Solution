using DAL.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Mvc;

namespace PL.Models
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Code is Required!")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name is Required!")]
        [MinLength(3, ErrorMessage = "Minimum Length Name is 3 characters")]
        [MaxLength(50, ErrorMessage = "Max Length Name is 50 characters")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfCreation { get; set; }

        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
