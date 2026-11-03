#nullable enable

using System.Collections.Generic;

namespace Documenso.Models;

public partial class EnvelopeItem
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public int Order { get; set; }

    public string DocumentDataId { get; set; } = null!;

    public virtual DocumentData DocumentData { get; set; } = null!;

    public string EnvelopeId { get; set; } = null!;

    public virtual Envelope Envelope { get; set; } = null!;

    public virtual ICollection<Field> Field { get; set; } = new List<Field>();
}
