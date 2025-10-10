namespace Backend.Domain.AggregatesModel.SessionAggregate;

using Common;
using Errors;

public class Session : AggregateRoot
{
    [Obsolete("Only for EF", true)]
    public Session()
    {
    }

    private Session(Guid userId, string ip, string agent)
    {
        UserId = userId;
        IsDeleted = false;
        Ip = ip.Trim();
        Agent = agent;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid UserId { get; private set; }

    public bool IsDeleted { get; private set; }

    public string Ip { get; private set; }

    public string Agent { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public static Session Create(Guid userId, string ip, string agent)
    {
        if (userId == Guid.Empty)
            throw DomainErrors.General.ValueIsRequired(nameof(UserId));

        if (string.IsNullOrWhiteSpace(ip))
            throw DomainErrors.General.ValueIsRequired(nameof(Ip));

        if (string.IsNullOrWhiteSpace(agent))
            throw DomainErrors.General.ValueIsRequired(nameof(Agent));

        return new Session(userId, ip, agent);
    }

    public void Delete() => IsDeleted = true;
}