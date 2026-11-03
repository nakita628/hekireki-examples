#nullable enable

namespace Documenso.Models;

public partial class DocumentData
{
    public string Id { get; set; } = null!;

    public DocumentDataType Type { get; set; }

    public string Data { get; set; } = null!;

    public string InitialData { get; set; } = null!;

    public virtual EnvelopeItem? EnvelopeItem { get; set; }
}
