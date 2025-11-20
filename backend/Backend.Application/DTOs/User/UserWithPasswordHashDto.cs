namespace Backend.Application.DTOs.User;

using System;
using Domain.AggregatesModel.UserAggregate;

public sealed record UserWithPasswordHashDto(
    Guid Id,
    string PasswordHash,
    string Login,
    bool IsDeleted,
    string Role,
    DateTime CreatedAt);