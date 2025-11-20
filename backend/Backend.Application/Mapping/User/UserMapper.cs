namespace Backend.Application.Mapping.User;

using Backend.Application.DTOs.User;
using Domain.AggregatesModel.UserAggregate;

public static class UserMapper
{
    public static UserDto MapToUserDto(User user)
    {
        return new UserDto(
            Id: user.Id,
            Login: user.Login,
            IsDeleted: user.IsDeleted,
            Role: user.Role.Value,
            CreatedAt: user.CreatedAt);
    }

    public static UserWithPasswordHashDto MapToUserWithPasswordHashDto(User user)
    {
        return new UserWithPasswordHashDto(
            Id: user.Id,
            PasswordHash: user.PasswordHash,
            Login: user.Login,
            IsDeleted: user.IsDeleted,
            Role: user.Role.Value,
            CreatedAt: user.CreatedAt);
    }
}