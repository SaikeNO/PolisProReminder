using PolisProReminder.Domain.Interfaces;

namespace PolisProReminder.Infrastructure.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> NotDeleted<T>(this IQueryable<T> query) where T : class, ISoftDeletable => query.Where(e => !e.IsDeleted);
    public static IQueryable<T> CreatedByAgent<T>(this IQueryable<T> query, Guid AgentId) where T : class, ICreatedBy => query.Where(q => q.CreatedByAgentId == AgentId);

}
