namespace Backend.Application.DTOs.Session;

using System;

public sealed record SessionDto(
    Guid Id,
    Guid UserId,
    bool IsDeleted,
    string Ip,
    string Agent,
    DateTime CreatedAt);