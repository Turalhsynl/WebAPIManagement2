using Application.CQRS.Users.ResponseDtos;
using Common.GlobalResponses.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Users.Handlers;

public class GetById
{
    public class Query : IRequest<Result<GetByIdDto>>
    {
        public int Id { get; set; }
    }

    public class Handler(IUnitOfWork unitOfWork) : IRequestHandler<Query, Result<GetByIdDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<GetByIdDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var currentUser = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);
            if (currentUser == null)
            {
                return new Result<GetByIdDto>() { Errors = ["User not found"], IsSuccess = true };
            }
            GetByIdDto response = new()
            {
                Id = currentUser.Id,
                Name = currentUser.Name,
                Surname = currentUser.Surname,
                Email = currentUser.Email,
                Username = currentUser.Username,
                FatherName = currentUser.FatherName,
                Address = currentUser.Address,
                MobilePhone = currentUser.MobilePhone,
                CardNumber = currentUser.CardNumber,
                TableNumber = currentUser.TableNumber,
                Birthdate = currentUser.Birthdate,
                DateOfEmployment = currentUser.DateOfEmployment,
                DateOfDismissal = currentUser.DateOfDismissal,
                Note = currentUser.Note
            };
            return new Result<GetByIdDto> { Data = response, Errors = [], IsSuccess = true };
        }
    }
}
