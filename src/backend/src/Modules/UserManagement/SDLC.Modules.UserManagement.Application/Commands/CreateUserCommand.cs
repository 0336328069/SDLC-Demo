using MediatR;
using SDLC.Modules.UserManagement.Application.DTOs;

namespace SDLC.Modules.UserManagement.Application.Commands;

public record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string Role
) : IRequest<UserDto>; 