using MediatR;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Application.Policies.Notifications;

public record PolicyCreatedNotification(Policy Policy) : INotification
{
    public Policy Policy { get; set; } = Policy;
}
