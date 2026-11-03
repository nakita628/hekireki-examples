#nullable enable

namespace Typebot.Models;

public partial class SetVariableHistoryItem
{
    public virtual Result Result { get; set; } = null!;

    public string ResultId { get; set; } = null!;

    public int Index { get; set; }

    public string VariableId { get; set; } = null!;

    public string BlockId { get; set; } = null!;

    public int? BlockIndex { get; set; }

    public string Value { get; set; } = null!;
}
