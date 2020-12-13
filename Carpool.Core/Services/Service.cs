using System;
using Carpool.Core.UnitOfWork;

namespace Carpool.Core.Services
{
    public abstract class Service
    {
        protected readonly IUnitOfWork unitOfWork;

        public Service(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
    }
}
