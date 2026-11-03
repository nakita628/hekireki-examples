#nullable enable

using System;
using System.Collections.Generic;

namespace Trigger.Models;

public partial class SecretReference
{
    public string Id { get; set; } = null!;

    public string Key { get; set; } = null!;

    public SecretStoreProvider Provider { get; set; } = SecretStoreProvider.Database;

    public virtual ICollection<EnvironmentVariableValue> EnvironmentVariableValues { get; set; } = new List<EnvironmentVariableValue>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<OrganizationIntegration> OrganizationIntegration { get; set; } = new List<OrganizationIntegration>();

    public virtual ICollection<User> User { get; set; } = new List<User>();
}
