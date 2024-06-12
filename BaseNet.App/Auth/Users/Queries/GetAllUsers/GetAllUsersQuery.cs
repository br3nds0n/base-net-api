using MediatR;

namespace BaseNet.App.Auth.Users.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserDTO>>
    {
    }
}