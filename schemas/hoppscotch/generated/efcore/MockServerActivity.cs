#nullable enable

using System;

namespace Hoppscotch.Models;

public partial class MockServerActivity
{
    public string Id { get; set; } = null!;

    public string MockServerID { get; set; } = null!;

    public MockServerAction Action { get; set; }

    public string? PerformedBy { get; set; }

    public DateTime PerformedAt { get; set; }

    public virtual MockServer MockServer { get; set; } = null!;
}
