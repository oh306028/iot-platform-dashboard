using IoTPlatform.Application.DTOs.Users;
using IoTPlatform.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace IoTPlatform.Application.Mapping;

[Mapper]
public partial class UserMapper
{
    [MapperIgnoreSource(nameof(User.PasswordHash))]
    public partial UserResponse ToResponse(User user);

    public partial IEnumerable<UserResponse> ToResponseList(IEnumerable<User> users);
}
