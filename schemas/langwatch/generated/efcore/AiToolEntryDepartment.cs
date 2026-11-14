#nullable enable

namespace LangWatch.Models;

public partial class AiToolEntryDepartment
{
    public string Id { get; set; } = null!;

    public string EntryId { get; set; } = null!;

    public string DepartmentId { get; set; } = null!;

    public virtual AiToolEntry Entry { get; set; } = null!;
}
