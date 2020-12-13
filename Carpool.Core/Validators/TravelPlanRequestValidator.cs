using Carpool.Core.Model.Entities;
using Carpool.Core.Requests;
using Carpool.Core.UnitOfWork;
using Carpool.Core.Validators.Extensions;
using FluentValidation;

namespace Carpool.Core.Validators
{
    public class TravelPlanRequestValidator<T> : AbstractValidator<T> where T : TravelPlanRequest
    {
        public static readonly int MAX_LOCATION_LENGTH = 100;

        public TravelPlanRequestValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.StartLocation)
                .NotEmpty()
                .WithMessage("Start location is required.")
                .NotEqual(x => x.EndLocation)
                .WithMessage("Start location can not be equal to the end location.")
                .MaximumLength(MAX_LOCATION_LENGTH)
                .WithMessage($"Start location must be less than {MAX_LOCATION_LENGTH} characters long.");

            RuleFor(x => x.EndLocation)
                .NotEmpty()
                .WithMessage("End location is required.")
                .MaximumLength(MAX_LOCATION_LENGTH)
                .WithMessage($"End location must be less than {MAX_LOCATION_LENGTH} characters long.");

            RuleFor(x => x.StartDate)
                .NotNull()
                .WithMessage("Start date is required.");

            RuleFor(x => x.EndDate)
                .NotNull()
                .WithMessage("End date is required.")
                .GreaterThanOrEqualTo(x => x.StartDate)
                .WithMessage("End date must be after or equal to end date.");

            RuleFor(x => x.EmployeeIds)
                .NotEmpty()
                .WithMessage("Passenger list must not be empty.")
                .MustHaveDriver(unitOfWork)
                .MustBeAvailable(unitOfWork);

            RuleFor(x => x.CarId)
                .NotEmpty()
                .WithMessage("Car is required.")
                .MustExist<T, Car, long>(unitOfWork)
                .DependentRules(() =>
                {
                    RuleFor(x => x.CarId)
                        .MustHaveEnoughCapacity(unitOfWork)
                        .MustBeAvailable(unitOfWork);
                });
        }
    }
}
