#nullable enable

namespace Calcom.Models;

public partial class Avatar
{
    public int TeamId { get; set; } = 0;

    public int UserId { get; set; } = 0;

    public string Data { get; set; } = null!;

    public string ObjectKey { get; set; } = null!;

    public bool IsBanner { get; set; } = false;
}
