#nullable enable

namespace Teable.Models;

public partial class ComputedUpdateStageLedger
{
    public string ScopeId { get; set; } = null!;

    public string Kind { get; set; } = null!;

    public string TableId { get; set; } = null!;

    public string RecordId { get; set; } = null!;

    public long Seq { get; set; } = 0L;
}
