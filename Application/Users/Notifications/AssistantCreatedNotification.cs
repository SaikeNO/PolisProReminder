using MediatR;

namespace PolisProReminder.Application.Users.Notifications;

public record AssistantCreatedNotification(Guid UserId) : INotification;
