using System;
using System.Threading.Tasks;
using Carpool.Core.Model.Entities;
using Carpool.Core.Repositories;
using Carpool.Core.UnitOfWork;
using Carpool.Infrastructure.EfModels;
using Carpool.Infrastructure.EfRepositories;

namespace Carpool.Infrastructure.EfUnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private static readonly string REPOSITORY_NOT_FOUND_ERROR = "Repository for requested type does not exist.";

        private readonly CarpoolContext context;

        private ICarRepository _carRepository;
        private IEmployeeRepository _employeeRepository;
        private ITravelPlanRepository _travelPlanRepository;
        private IEmployeeTravelPlanMappingRepository _employeeTravelPlanMappingRepository;

        public UnitOfWork(CarpoolContext context)
        {
            this.context = context;
        }

        public ICarRepository Cars
        {
            get
            {
                if(_carRepository is null)
                {
                    _carRepository = new CarRepository(context);
                }
                return _carRepository;
            }
        }

        public IEmployeeRepository Employees
        {
            get
            {
                if (_employeeRepository is null)
                {
                    _employeeRepository = new EmployeeRepository(context);
                }
                return _employeeRepository;
            }
        }

        public ITravelPlanRepository TravelPlans
        {
            get
            {
                if (_travelPlanRepository is null)
                {
                    _travelPlanRepository = new TravelPlanRepository(context);
                }
                return _travelPlanRepository;
            }
        }
        public IEmployeeTravelPlanMappingRepository EmployeeTravelPlanMappings
        {
            get
            {
                if (_employeeTravelPlanMappingRepository is null)
                {
                    _employeeTravelPlanMappingRepository = new EmployeeTravelPlanMappingRepository(context);
                }
                return _employeeTravelPlanMappingRepository;
            }
        }

        public async Task Commit()
        {
            await context.SaveChangesAsync();
        }

        public IRepository<TEntity, TPrimaryKey> GetRepository<TEntity, TPrimaryKey>() where TEntity : class
        {
            var type = typeof(TEntity);

            switch(true)
            {
                case bool _ when type == typeof(Car):
                    return Cars as IRepository<TEntity, TPrimaryKey>;
                case bool _ when type == typeof(Employee):
                    return Employees as IRepository<TEntity, TPrimaryKey>;
                case bool _ when type == typeof(TravelPlan):
                    return TravelPlans as IRepository<TEntity, TPrimaryKey>;
                case bool _ when type == typeof(EmployeeTravelPlanMapping):
                    return EmployeeTravelPlanMappings as IRepository<TEntity, TPrimaryKey>;
                default:
                    throw new ArgumentException(REPOSITORY_NOT_FOUND_ERROR);
            }
        }
    }
}
