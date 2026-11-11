#nullable enable

namespace LiteLLM.Models;

public partial class LiteLLMClaudeCodePluginUpload
{
    public string PluginName { get; set; } = null!;

    public byte[] Content { get; set; } = null!;

    public virtual LiteLLMClaudeCodePluginTable Plugin { get; set; } = null!;
}
