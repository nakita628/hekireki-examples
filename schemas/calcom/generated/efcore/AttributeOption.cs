#nullable enable

using System.Collections.Generic;

namespace Calcom.Models;

public partial class AttributeOption
{
    public string Id { get; set; } = null!;

    public virtual Attribute Attribute { get; set; } = null!;

    public string AttributeId { get; set; } = null!;

    public string Value { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public bool IsGroup { get; set; } = false;

    public List<string>? Contains { get; set; }

    public virtual ICollection<AttributeToUser> AssignedUsers { get; set; } = new List<AttributeToUser>();
}
