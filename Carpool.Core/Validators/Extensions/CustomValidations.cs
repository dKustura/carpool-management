using Carpool.Core.Requests;
using Carpool.Core.UnitOfWork;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carpool.Core.Validators.Extensions
{
    public static class CustomValidations
    {
        public static IRuleBuilderOptions<T, IEnumerable<long>> MustHaveDriver<T>
            (this IRuleBuilder<T, IEnumerable<long>> ruleBuilder, IUnitOfWork unitOfWork) where T : TravelPlanRequest
        {
            return ruleBuilder.MustAsync(async (employeeIds, cancellation) =>
            {
                foreach (var employeeId in employeeIds)
                {

                    var employee = await unitOfWork.Employees.GetById(employeeId);
                    if (employee.IsDriver) return true;
                }
                return false;
            }).WithMessage("At least one of the employees must be a driver.");
        }

        public static IRuleBuilderOptions<T, IEnumerable<long>> MustBeAvailable<T>
            (this IRuleBuilder<T, IEnumerable<long>> ruleBuilder, IUnitOfWork unitOfWork) where T : TravelPlanRequest
        {
            return ruleBuilder.MustAsync(async (travelPlan, employeeIds, context, cancellation) =>
            {
                long? excludedTravelPlanId = null;
                if (travelPlan is TravelPlanUpdateRequest castedTravelPlan)
                {
                    excludedTravelPlanId = castedTravelPlan.TravelPlanId;
                }

                var unavailableEmployees = await GetUnavailableEmployeeIds(travelPlan, employeeIds, unitOfWork, excludedTravelPlanId);

                context.MessageFormatter
                    .AppendArgument("UnavailableEmployees", string.Join(", ", unavailableEmployees));

                return !unavailableEmployees.Any();
            }).WithMessage("Employees with IDs = ({UnavailableEmployees}) are not available for given dates.");
        }

        //public static IRuleBuilderOptions<TravelPlanUpdateRequest, IEnumerable<long>> MustBeAvailable
        //    (this IRuleBuilder<TravelPlanUpdateRequest, IEnumerable<long>> ruleBuilder, IUnitOfWork unitOfWork)
        //{
        //    return ruleBuilder.MustAsync(async (travelPlan, employeeIds, context, cancellation) =>
        //    {
        //        var unavailableEmployees = await GetUnavailableEmployeeIds(travelPlan, employeeIds, unitOfWork, travelPlan.TravelPlanId);

        //        context.MessageFormatter
        //            .AppendArgument("UnavailableEmployees", string.Join(", ", unavailableEmployees));

        //        return !unavailableEmployees.Any();
        //    }).WithMessage("Employees with IDs = ({UnavailableEmployees}) are not available for given dates.");
        //}

        public static IRuleBuilderOptions<T, long> MustHaveEnoughCapacity<T>
            (this IRuleBuilder<T, long> ruleBuilder, IUnitOfWork unitOfWork) where T : TravelPlanRequest
        {
            return ruleBuilder.MustAsync(async (travelPlan, carId, context, cancellation) =>
            {
                var numOfEmployees = travelPlan.EmployeeIds.Count();
                var carModel = await unitOfWork.Cars.GetById(carId);

                context.MessageFormatter
                    .AppendArgument("NumberOfEmployees", numOfEmployees)
                    .AppendArgument("CarCapacity", carModel.Capacity);

                return numOfEmployees <= carModel.Capacity;
            }).WithMessage("Number of employees ({NumberOfEmployees}) exceeds the car capacity ({CarCapacity}).");
        }

        public static IRuleBuilderOptions<T, long> MustBeAvailable<T>
            (this IRuleBuilder<T, long> ruleBuilder, IUnitOfWork unitOfWork) where T : TravelPlanRequest
        {
            return ruleBuilder.MustAsync(async (travelPlan, carId, context, cancellation) =>
            {
                long? excludedTravelPlanId = null;
                if (travelPlan is TravelPlanUpdateRequest castedTravelPlan)
                {
                    excludedTravelPlanId = castedTravelPlan.TravelPlanId;
                }
                return await IsCarAvailable(travelPlan, carId, unitOfWork, excludedTravelPlanId);
            }).WithMessage("Car with ID = {PropertyValue} is not available during selected dates.");
        }

        //public static IRuleBuilderOptions<TravelPlanUpdateRequest, long> MustBeAvailable
        //    (this IRuleBuilder<TravelPlanUpdateRequest, long> ruleBuilder, IUnitOfWork unitOfWork)
        //{
        //    return ruleBuilder.MustAsync(async (travelPlan, carId, context, cancellation) =>
        //    {
        //        return await IsCarAvailable(travelPlan, carId, unitOfWork, travelPlan.TravelPlanId);
        //    }).WithMessage("Car with ID = {PropertyValue} is not available during selected dates.");
        //}

        public static IRuleBuilderOptions<T, TPrimaryKey> MustExist<T, TEntity, TPrimaryKey>
            (this IRuleBuilder<T, TPrimaryKey> ruleBuilder, IUnitOfWork unitOfWork) where TEntity : class
        {
            return ruleBuilder.MustAsync(async (id, cancellation) =>
            {
                var entity = await unitOfWork.GetRepository<TEntity, TPrimaryKey>().GetById(id);
                return !(entity is null);
            }).WithMessage($"{typeof(TEntity).Name} with ID = {{PropertyValue}} does not exists.");
        }

        public static bool IsBetweenDates(this DateTime date, DateTime start, DateTime end)
        {
            return date.Date >= start.Date && date.Date <= end.Date;
        }

        public static async Task<IList<long>> GetUnavailableEmployeeIds(TravelPlanRequest request, 
            IEnumerable<long> employeeIds, IUnitOfWork unitOfWork, long? excludedTravelPlanId = null)
        {
            var unavailableEmployees = new List<long>();

            foreach (var employeeId in employeeIds)
            {
                var travelPlansForEmployee = await unitOfWork.TravelPlans.GetByEmployeeId(employeeId, excludedTravelPlanId);

                var isAvailable = !travelPlansForEmployee.Any(tp => request.StartDate.IsBetweenDates(tp.StartDate, tp.EndDate)
                    || request.EndDate.IsBetweenDates(tp.StartDate, tp.EndDate));
                if (!isAvailable)
                {
                    unavailableEmployees.Add(employeeId);
                }
            }

            return unavailableEmployees;
        }

        public static async Task<bool> IsCarAvailable(TravelPlanRequest request, long carId, 
            IUnitOfWork unitOfWork, long? excludedTravelPlanId = null)
        {
            var travelPlansForCar = await unitOfWork.TravelPlans.GetByCarId(carId, excludedTravelPlanId);
            var isAvailable = !travelPlansForCar.Any(tp => request.StartDate.IsBetweenDates(tp.StartDate, tp.EndDate)
                || request.EndDate.IsBetweenDates(tp.StartDate, tp.EndDate));

            return isAvailable;
        }
    }
}
