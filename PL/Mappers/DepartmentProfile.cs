using AutoMapper;
using DAL.Entities;
using PL.Models;

namespace PL.Mappers
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<DepartmentViewModel, Department>().ReverseMap();
        }
    }
}
