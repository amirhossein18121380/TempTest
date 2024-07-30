using Application.Profiles;
using Domain.Entities.User;

namespace Application.Features.Users.Queries.GetUsers;

public record GetUsersQueryResponse : ICreateMapper<User>
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public int UserId { get; set; }
}