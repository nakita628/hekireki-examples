#nullable enable

using System;
using System.Collections.Generic;

namespace Formbricks.Models;

public partial class ContactAttributeKey
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsUnique { get; set; } = false;

    public string Key { get; set; } = null!;

    public string? Name { get; set; }

    public string? Description { get; set; }

    public ContactAttributeType Type { get; set; } = ContactAttributeType.Custom;

    public ContactAttributeDataType DataType { get; set; } = ContactAttributeDataType.String;

    public virtual Workspace Workspace { get; set; } = null!;

    public string WorkspaceId { get; set; } = null!;

    public virtual ICollection<ContactAttribute> Attributes { get; set; } = new List<ContactAttribute>();

    public virtual ICollection<SurveyAttributeFilter> AttributeFilters { get; set; } = new List<SurveyAttributeFilter>();
}
