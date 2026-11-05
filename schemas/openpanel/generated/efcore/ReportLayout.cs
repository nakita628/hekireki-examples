#nullable enable

using System;

namespace OpenPanel.Models;

public partial class ReportLayout
{
    public Guid Id { get; set; }

    public Guid ReportId { get; set; }

    public virtual Report Report { get; set; } = null!;

    public int GridColumns { get; set; } = 12;

    public int X { get; set; } = 0;

    public int Y { get; set; } = 0;

    public int W { get; set; } = 8;

    public int H { get; set; } = 6;

    public int? MinW { get; set; } = 4;

    public int? MinH { get; set; } = 4;

    public int? MaxW { get; set; }

    public int? MaxH { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
