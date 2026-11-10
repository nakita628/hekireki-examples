#nullable enable

using System;

namespace OpenSaas.Models;

public partial class Logs
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Message { get; set; } = null!;

    public string Level { get; set; } = null!;
}
