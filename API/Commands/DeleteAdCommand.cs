using MediatR;

namespace API.Commands
{
    public class DeleteAdCommand : IRequest<int>
    {
        public int Id { get; }
        public bool IsActive { get; }
        public DeleteAdCommand(int id, bool isActive)
        {
            IsActive = isActive;
            Id = id;
        }

    }
}
