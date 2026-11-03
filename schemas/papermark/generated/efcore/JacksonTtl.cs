#nullable enable

namespace Papermark.Models;

public partial class JacksonTtl
{
    public string Key { get; set; } = null!;

    public long ExpiresAt { get; set; }
}
