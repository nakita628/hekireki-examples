#nullable enable

using System;

namespace OpenPanel.Models;

public partial class Salt
{
    public string Salt1 { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
