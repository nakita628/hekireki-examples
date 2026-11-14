#nullable enable

using System;

namespace LangWatch.Models;

public partial class GatewayConnectUpstream
{
    public string OrganizationId { get; set; } = null!;

    public string BaseUrl { get; set; } = null!;

    public string EncryptedToken { get; set; } = null!;

    public string InstanceId { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }
}
