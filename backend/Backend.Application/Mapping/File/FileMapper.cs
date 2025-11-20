namespace Backend.Application.Mapping.File;

using Backend.Application.DTOs.File;
using File = Domain.AggregatesModel.FileAggregate.File;

public static class FileMapper
{
    public static FileDto MapToFileDto(File file)
    {
        return new FileDto(
            Id: file.Id,
            CreatedAt: file.CreatedAt,
            OriginalName: file.OriginalName,
            FileSystemName: file.FileSystemName,
            Type: file.Type.Value);
    }
}