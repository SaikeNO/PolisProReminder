using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Interfaces;

namespace PolisProReminder.Infrastructure.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> NotDeleted<T>(this IQueryable<T> query) where T : class, ISoftDeletable => query.Where(e => !e.IsDeleted);
    public static IQueryable<T> CreatedByAgent<T>(this IQueryable<T> query, Guid AgentId) where T : class, ICreatedBy => query.Where(q => q.CreatedByAgentId == AgentId);
    public static IQueryable<IndividualInsurer> FilterBySearchPhrase(this IQueryable<IndividualInsurer> query, string? searchPhraseLower)
    {
        if (string.IsNullOrEmpty(searchPhraseLower))
        {
            return query;
        }

        return query.Where(i =>
            i.LastName.ToLower().Contains(searchPhraseLower)
            || i.FirstName.ToLower().Contains(searchPhraseLower)
            || i.Email.ToLower().Contains(searchPhraseLower)
            || i.Pesel.ToLower().Contains(searchPhraseLower)
            || i.PhoneNumber.ToLower().Contains(searchPhraseLower));
    }

    public static IQueryable<BusinessInsurer> FilterBySearchPhrase(this IQueryable<BusinessInsurer> query, string? searchPhraseLower)
    {
        if (string.IsNullOrEmpty(searchPhraseLower))
        {
            return query;
        }

        return query.Where(b =>
            b.Name.ToLower().Contains(searchPhraseLower)
            || b.Nip.ToLower().Contains(searchPhraseLower)
            || b.Regon.ToLower().Contains(searchPhraseLower)
            || b.Email.ToLower().Contains(searchPhraseLower)
            || b.PhoneNumber.ToLower().Contains(searchPhraseLower));
    }
}
