#nullable enable

using System;
using System.Collections.Generic;

namespace Trigger.Models;

public partial class OrganizationDataStore
{
    public string Id { get; set; } = null!;

    public string Key { get; set; } = null!;

    public List<string>? OrganizationIds { get; set; }

    public DataStoreKind Kind { get; set; }

    public string Config { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
