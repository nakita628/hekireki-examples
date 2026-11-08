#nullable enable

namespace TypeHero.Models;

public partial class ReportIssue
{
    public int Id { get; set; }

    public int ReportId { get; set; }

    public virtual Report Report { get; set; } = null!;

    public IssueType Type { get; set; }
}
