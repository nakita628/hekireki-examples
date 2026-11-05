#nullable enable

using System;

namespace Chibisafe.Models;

public partial class Statistics
{
    public int Id { get; set; }

    public int? BatchId { get; set; }

    public string? Type { get; set; }

    public string? Data { get; set; }

    public DateTime CreatedAt { get; set; }
}
