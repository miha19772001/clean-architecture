namespace Backend.Application.APIHandlers.File.GetImage;

using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;

public sealed record GetImageRequest(
    Guid Id)
    : IRequest<PhysicalFileResult>;