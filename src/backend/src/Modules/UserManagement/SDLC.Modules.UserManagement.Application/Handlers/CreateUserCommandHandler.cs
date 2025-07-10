using MediatR;
using SDLC.Modules.UserManagement.Application.Commands;
using SDLC.Modules.UserManagement.Application.DTOs;
using SDLC.Modules.UserManagement.Application.Services;
using SDLC.Modules.UserManagement.Domain.Entities;
using SDLC.Modules.UserManagement.Domain.Services;
using SDLC.Shared.Kernel.ValueObjects;

namespace SDLC.Modules.UserManagement.Application.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Validate email uniqueness
        if (await _userRepository.ExistsByEmailAsync(request.Email, cancellationToken))
        {
            throw new InvalidOperationException($"User with email '{request.Email}' already exists.");
        }

        // Parse role
        if (!Enum.TryParse<UserRole>(request.Role, out var userRole))
        {
            throw new ArgumentException($"Invalid role: {request.Role}");
        }

        // Create user
        var email = new Email(request.Email);
        var hashedPassword = _passwordHasher.HashPassword(request.Password);
        
        var user = new User(
            request.FirstName,
            request.LastName,
            email,
            hashedPassword,
            userRole);

        var createdUser = await _userRepository.AddAsync(user, cancellationToken);

        return new UserDto(
            createdUser.Id,
            createdUser.FirstName,
            createdUser.LastName,
            createdUser.Email.Value,
            createdUser.Role.ToString(),
            createdUser.IsActive,
            createdUser.LastLoginAt,
            createdUser.CreatedAt);
    }
} 