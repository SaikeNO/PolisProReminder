namespace PolisProReminder.Application.Insurers.Dtos;

public record InsurerBasicInfoDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
