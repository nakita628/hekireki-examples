#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMUISettings
{
    public string Id { get; set; } = "ui_settings";

    public string UiSettings { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
