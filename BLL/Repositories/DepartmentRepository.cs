using BLL.Interfaces;
using DAL.Contexts;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly MVCAppDbContext _dbContext;

        public DepartmentRepository(MVCAppDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
