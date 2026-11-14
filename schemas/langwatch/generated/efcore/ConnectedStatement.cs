#nullable enable

using System;

namespace LangWatch.Models;

public partial class ConnectedStatement
{
    public string Id { get; set; } = null!;

    public string AccountId { get; set; } = null!;

    public DateTime Month { get; set; }

    public DateTime SentAt { get; set; }
}
