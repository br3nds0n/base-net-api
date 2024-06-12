using AutoMapper;
using BaseNet.Domain.Entities.User;

namespace BaseNet.App.Auth.Users
{
    [AutoMap(typeof(User), ReverseMap = true)]
    public class UserDTO
    {
        public Guid UserId { get; set; }
    }
}