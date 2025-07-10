using MediatR;
using SDLC.Modules.UserManagement.Application.DTOs;

namespace SDLC.Modules.UserManagement.Application.Queries;

public record GetAllUsersQuery(
    int PageNumber = 1,
    int PageSize = 10,
    string? SearchTerm = null,
    bool? IsActive = null
) : IRequest<PagedResult<UserDto>>; 