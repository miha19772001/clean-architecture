namespace Backend.Application.Services.Account;

using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.AggregatesModel.UserAggregate;

public interface IAccountService
{
    Task SignInAsync(Guid userId, CancellationToken cancellationToken = default);

    Task SignOutAsync(CancellationToken cancellationToken = default);

    Task<Guid> GetCurrentUserIdAsync(CancellationToken cancellationToken = default);
}