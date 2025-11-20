namespace Backend.Application.Mapping.Session;

using Backend.Application.DTOs.Session;
using Domain.AggregatesModel.SessionAggregate;

public static class SessionMapper
{
    public static SessionDto MapToSessionDto(Session session)
    {
        return new SessionDto(
            Id: session.Id,
            UserId: session.UserId,
            IsDeleted: session.IsDeleted,
            Ip: session.Ip,
            Agent: session.Agent,
            CreatedAt: session.CreatedAt);
    }
}