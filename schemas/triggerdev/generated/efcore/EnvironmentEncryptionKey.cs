#nullable enable

using System;
using System.Collections.Generic;

namespace Trigger.Models;

public partial class EnvironmentEncryptionKey
{
    public string Id { get; set; } = null!;

    public string EnvironmentId { get; set; } = null!;

    public virtual RuntimeEnvironment Environment { get; set; } = null!;

    public string WrappedKey { get; set; } = null!;

    public EnvironmentKeyWrappingProvider WrappingProvider { get; set; }

    public string WrappingKeyReference { get; set; } = null!;

    public int Revision { get; set; } = 0;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<EnvironmentEncryptionPolicy> ActiveFor { get; set; } = new List<EnvironmentEncryptionPolicy>();
}
