#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum GatewayProvider
{
    [PgName("openai")]
    Openai,

    [PgName("anthropic")]
    Anthropic,

    [PgName("bedrock")]
    Bedrock,
}
