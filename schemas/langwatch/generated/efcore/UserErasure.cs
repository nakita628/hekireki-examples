#nullable enable

using System;

namespace LangWatch.Models;

public partial class UserErasure
{
    public string Id { get; set; } = null!;

    public DateTime AccountCreatedAt { get; set; }

    public DateTime ErasedAt { get; set; }
}
