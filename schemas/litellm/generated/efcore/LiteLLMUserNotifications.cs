#nullable enable

using System.Collections.Generic;

namespace LiteLLM.Models;

public partial class LiteLLMUserNotifications
{
    public string RequestId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public List<string>? Models { get; set; }

    public string Justification { get; set; } = null!;

    public string Status { get; set; } = null!;
}
