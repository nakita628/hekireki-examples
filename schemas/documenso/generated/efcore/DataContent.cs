#nullable enable

using System.Collections.Generic;

namespace Documenso.Models;

public partial class DataContent
{
    public string Id { get; set; } = null!;

    public DocumentDataType Type { get; set; }

    public string Data { get; set; } = null!;

    public string Metadata { get; set; } = null!;

    public virtual ICollection<EnvelopeContent> EnvelopeContents { get; set; } = new List<EnvelopeContent>();
}
