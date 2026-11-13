#nullable enable

namespace InboxZero.Models;

public partial class BulkInboxActionThread
{
    public string JobId { get; set; } = null!;

    public string ThreadId { get; set; } = null!;

    public virtual BulkInboxActionJob Job { get; set; } = null!;
}
