#nullable enable

namespace Langfuse.Models;

public partial class GatewayApiKeyAssociation
{
    public string ApiKeyId { get; set; } = null!;

    public virtual ApiKey ApiKey { get; set; } = null!;

    public string Metadata { get; set; } = "{}";
}
