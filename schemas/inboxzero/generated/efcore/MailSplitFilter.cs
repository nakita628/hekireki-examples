#nullable enable

namespace InboxZero.Models;

public partial class MailSplitFilter
{
    public string Id { get; set; } = null!;

    public MailSplitFilterKind Kind { get; set; }

    public string? Value { get; set; }

    public int Order { get; set; }

    public string MailSplitId { get; set; } = null!;

    public virtual MailSplit MailSplit { get; set; } = null!;
}
