#nullable enable

using System;

namespace Blinko.Models;

public partial class Cache
{
    public int Id { get; set; }

    public string Key { get; set; } = null!;

    public string Value { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
