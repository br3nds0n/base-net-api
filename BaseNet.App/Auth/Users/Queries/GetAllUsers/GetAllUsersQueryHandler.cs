using MediatR;
using AutoMapper;
using BaseNet.Domain.Entities.User;
using BaseNet.Libs.Data.SDK.Repositories;

namespace BaseNet.App.Auth.Users.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ReadingRepository<User> _userRepository;

        public GetAllUsersQueryHandler(IMapper mapper, ReadingRepository<User> userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDTO>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.ReadAll();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }
    }
}