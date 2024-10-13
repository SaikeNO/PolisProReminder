using Microsoft.EntityFrameworkCore;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Interfaces;

namespace PolisProReminder.Infrastructure.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> NotDeleted<T>(this IQueryable<T> query) where T : class, ISoftDeletable => query.Where(e => !e.IsDeleted);
    public static IQueryable<T> CreatedByAgent<T>(this IQueryable<T> query, Guid AgentId) where T : class, ICreatedBy => query.Where(q => q.CreatedByAgentId == AgentId);
    public static IQueryable<T> FilterByInsurer<T>(this IQueryable<T> query, string? searchPhraseLower, DbContext dbContext) where T : class
    {
        if (string.IsNullOrEmpty(searchPhraseLower))
        {
            return query;
        }

        return query.Where(e =>
            dbContext.Set<IndividualInsurer>()
                .Where(i => EF.Property<Guid>(e, "InsurerId") == i.Id)
                .Any(i => i.LastName.ToLower().Contains(searchPhraseLower)
                        || i.FirstName.ToLower().Contains(searchPhraseLower)
                        || i.Email.ToLower().Contains(searchPhraseLower)
                        || i.Pesel.ToLower().Contains(searchPhraseLower)
                        || i.PhoneNumber.ToLower().Contains(searchPhraseLower))
            ||
            dbContext.Set<BusinessInsurer>()
                .Where(b => EF.Property<Guid>(e, "InsurerId") == b.Id)
                .Any(b => b.Name.ToLower().Contains(searchPhraseLower)
                        || b.Nip.ToLower().Contains(searchPhraseLower)
                        || b.Regon.ToLower().Contains(searchPhraseLower)
                        || b.Email.ToLower().Contains(searchPhraseLower)
                        || b.PhoneNumber.ToLower().Contains(searchPhraseLower))
        );
    }
}
