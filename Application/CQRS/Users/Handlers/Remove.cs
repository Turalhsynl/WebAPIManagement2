using AutoMapper;
using Common.Exceptions;
using Common.GlobalResponses.Generics;
using Domain.Entities;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Users.Handlers;

public class Remove
{
    public class Command : IRequest<Result<Unit>>
    {
        public int Id { get; set; }
    }

    public class Handler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<Command, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var currentUser = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);
            if (currentUser == null) throw new BadRequestException("User not found or deleted.");

            var user = _mapper.Map<User>(request);

            user.DeletedBy = 1;
            var result = _unitOfWork.UserRepository.Remove(currentUser.Id, user.DeletedBy.Value);

            if (!result)
            {
                throw new Exception("Failed to remove user.");
            }

            return new Result<Unit>
            {
                Data = Unit.Value,
                Errors = new List<string>(),
                IsSuccess = true
            };
        }
    }
}
