using PolisProReminder.Domain.Constants;

namespace PolisProReminder.Application.Common;

public record PageRequest
{
    public string? SearchPhrase { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public string? SortBy { get; set; }
    public SortDirection SortDirection { get; set; }
}
