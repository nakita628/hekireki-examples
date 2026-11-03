#nullable enable

using System;

namespace Documenso.Models;

public partial class EmbedSignature
{
    public string Key { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string OrganisationId { get; set; } = null!;

    public virtual Organisation Organisation { get; set; } = null!;

    public int RecipientId { get; set; }

    public string Signature { get; set; } = null!;
}
