namespace Backend.Domain.AggregatesModel.FileAggregate;

using Common;
using Errors;
using System.Collections.Generic;

public class FileType : ValueObject
{
    public static readonly FileType Image = new(nameof(Image));
    public static readonly FileType Other = new(nameof(Other));

    private static readonly FileType[] FileTypes = [Image, Other];

    public string Value { get; }

    private FileType(string value)
    {
        Value = value;
    }

    public static FileType Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw DomainErrors.General.ValueIsRequired(nameof(FileType));

        var fileType = value.Trim().ToLower();

        var hasValue =
            FileTypes.Any(x => x.Value.Equals(fileType, StringComparison.CurrentCultureIgnoreCase));

        if (!hasValue)
            throw DomainErrors.General.ValueIsInvalid(nameof(FileType));

        return new FileType(fileType);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}