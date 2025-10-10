namespace Backend.Domain.AggregatesModel.FileAggregate;

using Errors;

public class Image : File
{
    [Obsolete("Only for EF", true)]
    public Image()
    {
    }

    private Image(string originalName, string fileSystemName)
        : base(originalName, fileSystemName, FileType.Image)
    {
    }

    public static Image Create(string originalName, string fileSystemName)
    {
        if (string.IsNullOrWhiteSpace(originalName))
            throw DomainErrors.General.ValueIsRequired(nameof(OriginalName));

        if (string.IsNullOrWhiteSpace(fileSystemName))
            throw DomainErrors.General.ValueIsRequired(nameof(FileSystemName));

        return new Image(originalName, fileSystemName);
    }
}