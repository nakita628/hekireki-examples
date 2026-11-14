#nullable enable

using System;

namespace LangWatch.Models;

public partial class GatewayTraceExportKey
{
    public string ProjectId { get; set; } = null!;

    public string ApiKeyId { get; set; } = null!;

    public string EncryptedToken { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
