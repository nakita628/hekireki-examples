#nullable enable

using System;

namespace OpenPanel.Models;

public partial class CodeMigration
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
