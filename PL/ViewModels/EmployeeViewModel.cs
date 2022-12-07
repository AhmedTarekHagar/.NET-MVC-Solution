using DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace PL.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is REQUIRED!")]
        [MaxLength(50, ErrorMessage = "Maximum Length is 50 characters")]
        [MinLength(3, ErrorMessage = "Minimum Length is 3 characters")]
        public string Name { get; set; }

        [Range(22, 30, ErrorMessage = "Age must be between 22 and 30")]
        public int? Age { get; set; }

        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}$", ErrorMessage = "Address must follow the pattern 123-street-city-country")]
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        [Range(4000, 8000)]
        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }

        public Department Department { get; set; }
    }
}
