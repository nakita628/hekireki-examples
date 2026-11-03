#nullable enable

using System;

namespace Formbricks.Models;

public partial class ContactAttribute
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ContactAttributeKey AttributeKey { get; set; } = null!;

    public string AttributeKeyId { get; set; } = null!;

    public virtual Contact Contact { get; set; } = null!;

    public string ContactId { get; set; } = null!;

    public string Value { get; set; } = null!;

    public double? ValueNumber { get; set; }

    public DateTime? ValueDate { get; set; }
}
