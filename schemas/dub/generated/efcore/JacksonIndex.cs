#nullable enable

namespace Dub.Models;

public partial class JacksonIndex
{
    public int Id { get; set; }

    public string Key { get; set; } = null!;

    public string StoreKey { get; set; } = null!;
}
