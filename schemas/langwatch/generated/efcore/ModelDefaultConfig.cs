#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class ModelDefaultConfig
{
    public string Id { get; set; } = null!;

    public string Config { get; set; } = null!;

    public string? AuthorId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<ModelDefaultConfigScope> Scopes { get; set; } = new List<ModelDefaultConfigScope>();

    public string OrganizationId { get; set; } = null!;
}
