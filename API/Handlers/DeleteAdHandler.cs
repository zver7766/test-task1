using System.Threading;
using System.Threading.Tasks;
using API.Commands;
using Core.Entities;
using Core.Intefraces;
using MediatR;

namespace API.Handlers
{
    public class DeleteAdHandler : IRequestHandler<DeleteAdCommand,int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(DeleteAdCommand request, CancellationToken cancellationToken)
        {
            var ad = await _unitOfWork.Repository<Advertisement>().GetByIdAsync(request.Id);

            if (ad == null) return 0;

            if (!request.IsActive)
                _unitOfWork.Repository<Advertisement>().Delete(ad);

            if (request.IsActive)
                ad.IsActive = !ad.IsActive;

            var result = await _unitOfWork.Complete();

            return result;
        }
    }
}
