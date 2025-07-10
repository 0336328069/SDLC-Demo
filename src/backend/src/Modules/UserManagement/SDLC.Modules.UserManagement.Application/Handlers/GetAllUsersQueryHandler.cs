using MediatR;
using SDLC.Modules.UserManagement.Application.DTOs;
using SDLC.Modules.UserManagement.Application.Queries;
using SDLC.Modules.UserManagement.Domain.Services;

namespace SDLC.Modules.UserManagement.Application.Handlers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, PagedResult<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<PagedResult<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(
            request.PageNumber, 
            request.PageSize, 
            request.SearchTerm, 
            request.IsActive, 
            cancellationToken);

        var totalCount = await _userRepository.GetTotalCountAsync(
            request.SearchTerm, 
            request.IsActive, 
            cancellationToken);

        var userDtos = users.Select(user => new UserDto(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email.Value,
            user.Role.ToString(),
            user.IsActive,
            user.LastLoginAt,
            user.CreatedAt)).ToList();

        return new PagedResult<UserDto>(
            userDtos,
            totalCount,
            request.PageNumber,
            request.PageSize);
    }
} 