#nullable enable

using System;

namespace Hoppscotch.Models;

public partial class MockServerLog
{
    public string Id { get; set; } = null!;

    public string MockServerID { get; set; } = null!;

    public string RequestMethod { get; set; } = null!;

    public string RequestPath { get; set; } = null!;

    public string RequestHeaders { get; set; } = null!;

    public string? RequestBody { get; set; }

    public string? RequestQuery { get; set; }

    public int ResponseStatus { get; set; }

    public string ResponseHeaders { get; set; } = null!;

    public string? ResponseBody { get; set; }

    public int ResponseTime { get; set; }

    public string? IpAddress { get; set; }

    public string? UserAgent { get; set; }

    public DateTime ExecutedAt { get; set; }

    public virtual MockServer MockServer { get; set; } = null!;
}
