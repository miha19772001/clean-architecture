namespace Backend.Domain.AggregatesModel.FileAggregate;

using Common;
using Errors;
using System.Collections.Generic;

public class FileType : ValueObject
{
    public static readonly FileType Image = new(nameof(Image));

    private static readonly FileType[] FileTypes = [Image];

    public string Value { get; private set; }

    private FileType(string value)
    {
        Value = value;
    }

    public static FileType Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw DomainErrors.General.ValueIsRequired(nameof(FileType));

        var fileType = input.Trim().ToLower();

        var isFileTypePresent =
            FileTypes.Any(ft => ft.Value.Equals(fileType, StringComparison.CurrentCultureIgnoreCase));

        if (!isFileTypePresent)
            throw DomainErrors.General.ValueIsInvalid(nameof(FileType));

        return new FileType(fileType);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}