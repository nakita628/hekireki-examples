#nullable enable

using System;

namespace Hoppscotch.Models;

public partial class InfraToken
{
    public string Id { get; set; } = null!;

    public string CreatorUid { get; set; } = null!;

    public string Label { get; set; } = null!;

    public string Token { get; set; } = null!;

    public DateTime? ExpiresOn { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }
}
