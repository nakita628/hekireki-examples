#nullable enable

using System;

namespace Postiz.Models;

public partial class MastraFavorites
{
    public string UserId { get; set; } = null!;

    public string EntityType { get; set; } = null!;

    public string EntityId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? CreatedAtZ { get; set; } = DateTime.UtcNow;
}
