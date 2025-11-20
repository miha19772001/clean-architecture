namespace Backend.Application.DTOs.File;

using System;

public sealed record FileDto(
    Guid Id,
    DateTime CreatedAt,
    string OriginalName,
    string FileSystemName,
    string Type);