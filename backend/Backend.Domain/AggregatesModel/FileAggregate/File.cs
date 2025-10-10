namespace Backend.Domain.AggregatesModel.FileAggregate;

using Common;
using Errors;

public class File : AggregateRoot
{
    [Obsolete("Only for EF", true)]
    public File()
    {
    }

    protected File(
        string originalName,
        string fileSystemName,
        FileType type)
    {
        CreatedAt = DateTime.UtcNow;
        OriginalName = originalName;
        FileSystemName = fileSystemName;
        Type = type;
    }

    public DateTime CreatedAt { get; private set; }

    public string OriginalName { get; private set; }

    public string FileSystemName { get; private set; }

    public FileType Type { get; private set; }

    public static File Create(string originalName, string fileSystemName, FileType type)
    {
        if (string.IsNullOrWhiteSpace(originalName))
            throw DomainErrors.General.ValueIsRequired(nameof(OriginalName));

        if (string.IsNullOrWhiteSpace(fileSystemName))
            throw DomainErrors.General.ValueIsRequired(nameof(FileSystemName));

        if (string.IsNullOrWhiteSpace(type.Value))
            throw DomainErrors.General.ValueIsRequired(nameof(FileType));

        return new File(originalName, fileSystemName, type);
    }
}