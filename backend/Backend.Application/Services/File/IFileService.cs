namespace Backend.Application.Services.File;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

public interface IFileService
{
    // Task SaveAsync(
    //     IFormFile file,
    //     string fileSystemName,
    //     FileType type,
    //     CancellationToken cancellationToken = default);

    Task<PhysicalFileResult> GetImageAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}