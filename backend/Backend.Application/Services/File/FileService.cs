namespace Backend.Application.Services.File;

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Backend.Application.Queries.File;
using Domain.AggregatesModel.FileAggregate;
using Domain.Errors;
using Infrastructure.Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.StaticFiles;

public class FileService(
    IMediator mediator,
    IConfiguration configuration)
    : IFileService
{
    public async Task<PhysicalFileResult> GetImageAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var file = await mediator.Send(
            new FindFileByIdQuery(id),
            cancellationToken);

        if (file is null)
            throw DomainErrors.File.NotFound();

        if (!string.Equals(file.Type, FileType.Image.Value, StringComparison.InvariantCultureIgnoreCase))
            throw DomainErrors.File.HasDifferentType();

        var fullPath = Path.Combine(GetFolder(file.Type), file.FileSystemName);

        if (!System.IO.File.Exists(fullPath))
            throw InfrastructureErrors.General.ErrorMessage("File does not exist");

        return new PhysicalFileResult(fullPath, GetMimeType(file.FileSystemName))
        {
            FileDownloadName = file.OriginalName,
        };
    }

    private static string GetMimeType(string filePath)
    {
        var provider = new FileExtensionContentTypeProvider();

        if (!provider.TryGetContentType(filePath, out var contentType))
            contentType = "application/octet-stream";

        return contentType;
    }

    private string GetFolder(string typeValue)
    {
        if (!TypeMapping.TryGetValue(typeValue, out var configKey))
            throw new InvalidOperationException($"Unknown file type: {typeValue}");

        var path = configuration[configKey];

        return string.IsNullOrEmpty(path)
            ? throw new InvalidOperationException($"Configuration key '{configKey}' is not set or empty")
            : path;
    }

    private static readonly Dictionary<string, string> TypeMapping = new(StringComparer.OrdinalIgnoreCase)
    {
        { FileType.Image.Value, "Paths:Images" },
        { FileType.Other.Value, "Paths:Other" },
    };
}