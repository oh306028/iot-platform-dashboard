using IoTPlatform.Application.DTOs.Users;

namespace IoTPlatform.Application.Interfaces;

public interface IUserService
{
    Task<UserResponse> RegisterAsync(RegisterUserRequest request);
    Task<LoginResponse> LoginAsync(LoginRequest request);
    Task<IEnumerable<UserResponse>> GetAllAsync();
}

