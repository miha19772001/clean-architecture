namespace Backend.Application.DTOs.User;

using System;
using Domain.AggregatesModel.UserAggregate;

public sealed record UserDto(
    Guid Id,
    string Login,
    bool IsDeleted,
    string Role,
    DateTime CreatedAt);