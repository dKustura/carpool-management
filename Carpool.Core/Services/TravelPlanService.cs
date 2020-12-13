using Carpool.Core.Exceptions;
using Carpool.Core.Model.Entities;
using Carpool.Core.Requests;
using Carpool.Core.Results;
using Carpool.Core.Services.Contracts;
using Carpool.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carpool.Core.Services
{
    public class TravelPlanService : Service, ITravelPlanService
    {
        public TravelPlanService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<TravelPlanResult> Create(TravelPlanCreateRequest request)
        {
            var employeeTravelPlanMappings = request.EmployeeIds.Select(employeeId => new EmployeeTravelPlanMapping
            {
                EmployeeId = employeeId,
            }).ToList();

            var travelPlan = new TravelPlan
            {
                StartLocation = request.StartLocation,
                EndLocation = request.EndLocation,
                StartDate = request.StartDate.Date,
                EndDate = request.EndDate.Date,
                CarId = request.CarId,
                EmployeeTravelPlanMappings = employeeTravelPlanMappings

            };

            await unitOfWork.TravelPlans.Add(travelPlan);
            await unitOfWork.Commit();

            return new TravelPlanResult(travelPlan);
        }

        public async Task Delete(long id)
        {
            var model = await unitOfWork.TravelPlans.GetById(id) ?? throw new EntityNotFoundException($"Travel plan with ID = {id} does not exist.");

            unitOfWork.TravelPlans.Delete(model);
            await unitOfWork.Commit();
        }

        public async Task<IEnumerable<TravelPlanResult>> GetAll()
        {
            var models = await unitOfWork.TravelPlans.GetAll();

            var results = models.Select(model => new TravelPlanResult(model));
            return results;
        }

        public async Task<TravelPlanResult> GetById(long id)
        {
            var model = await unitOfWork.TravelPlans.GetById(id) ?? throw new EntityNotFoundException($"Travel plan with ID = {id} does not exist.");
            return new TravelPlanResult(model);
        }

        public async Task Update(TravelPlanUpdateRequest request)
        {
            // TODO: write custom NotFound exception and middleware to catch it
            var model = await unitOfWork.TravelPlans.GetById(request.TravelPlanId) ?? throw new EntityNotFoundException($"Travel plan with ID = {request.TravelPlanId} does not exist.");

            model.StartLocation = request.StartLocation;
            model.EndLocation = request.EndLocation;
            model.StartDate = request.StartDate.Date;
            model.EndDate = request.EndDate.Date;
            model.CarId = request.CarId;

            var employeeTravelPlanMappings = request.EmployeeIds.Select(employeeId => new EmployeeTravelPlanMapping
            {
                EmployeeId = employeeId,
            }).ToList();

            // Delete old mappings before setting new ones
            unitOfWork.EmployeeTravelPlanMappings.DeleteRange(model.EmployeeTravelPlanMappings);
            model.EmployeeTravelPlanMappings = employeeTravelPlanMappings;

            unitOfWork.TravelPlans.Update(model);
            await unitOfWork.Commit();
        }
    }
}
