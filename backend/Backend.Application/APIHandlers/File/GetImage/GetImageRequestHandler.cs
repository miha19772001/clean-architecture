namespace Backend.Application.APIHandlers.File.GetImage;

using System.Threading;
using System.Threading.Tasks;
using Backend.Application.Services.File;
using MediatR;
using Microsoft.AspNetCore.Mvc;

public class GetImageRequestHandler(
    IFileService fileService)
    : IRequestHandler<GetImageRequest, PhysicalFileResult>
{
    public async Task<PhysicalFileResult> Handle(
        GetImageRequest request,
        CancellationToken cancellationToken = default)
    {
        var image = await fileService.GetImageAsync(request.Id,
            cancellationToken);

        return image;
    }
}