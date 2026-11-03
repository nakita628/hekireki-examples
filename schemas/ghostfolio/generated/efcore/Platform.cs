#nullable enable

using System.Collections.Generic;

namespace Ghostfolio.Models;

public partial class Platform
{
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string Url { get; set; } = null!;
}
