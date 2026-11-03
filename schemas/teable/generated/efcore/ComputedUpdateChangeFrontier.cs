#nullable enable

namespace Teable.Models;

public partial class ComputedUpdateChangeFrontier
{
    public string ScopeId { get; set; } = null!;

    public string Kind { get; set; } = null!;

    public string TableId { get; set; } = null!;

    public string RecordId { get; set; } = null!;

    public string FieldId { get; set; } = null!;
}
