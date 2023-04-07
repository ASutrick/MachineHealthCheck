using MachineHealthCheck.Domain.Interfaces;
using MachineHealthCheck.Domain.Interfaces.Services;

namespace HealthCheck.Host
{
    public class VerificationService : IVerificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public VerificationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }
        public Task Verify(string key)
        {

        }
    }
}
