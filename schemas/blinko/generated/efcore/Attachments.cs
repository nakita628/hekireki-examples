#nullable enable

using System;

namespace Blinko.Models;

public partial class Attachments
{
    public int Id { get; set; }

    public bool IsShare { get; set; } = false;

    public string SharePassword { get; set; } = "";

    public string Name { get; set; } = "";

    public string Path { get; set; } = "";

    public decimal Size { get; set; } = 0m;

    public string Type { get; set; } = "";

    public int? NoteId { get; set; }

    public int? AccountId { get; set; }

    public int SortOrder { get; set; } = 0;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? PerfixPath { get; set; } = "";

    public int? Depth { get; set; }

    public string? Metadata { get; set; }

    public virtual Notes? Note { get; set; }

    public virtual Accounts? Account { get; set; }
}
