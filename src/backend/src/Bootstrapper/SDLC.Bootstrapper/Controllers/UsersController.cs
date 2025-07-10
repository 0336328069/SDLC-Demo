using MediatR;
using Microsoft.AspNetCore.Mvc;
using SDLC.Modules.UserManagement.Application.Commands;
using SDLC.Modules.UserManagement.Application.DTOs;
using SDLC.Modules.UserManagement.Application.Queries;

namespace SDLC.Bootstrapper.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get all users with pagination and filtering
    /// </summary>
    /// <param name="pageNumber">Page number (default: 1)</param>
    /// <param name="pageSize">Page size (default: 10)</param>
    /// <param name="searchTerm">Search term for name or email</param>
    /// <param name="isActive">Filter by active status</param>
    /// <returns>Paginated list of users</returns>
    [HttpGet]
    public async Task<ActionResult<PagedResult<UserDto>>> GetUsers(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? searchTerm = null,
        [FromQuery] bool? isActive = null)
    {
        var query = new GetAllUsersQuery(pageNumber, pageSize, searchTerm, isActive);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Create a new user
    /// </summary>
    /// <param name="request">User creation data</param>
    /// <returns>Created user</returns>
    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserDto request)
    {
        var command = new CreateUserCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password,
            request.Role);

        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetUsers), new { id = result.Id }, result);
    }
} 