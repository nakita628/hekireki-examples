#nullable enable

using System;

namespace AnythingLLM.Models;

public partial class EmbedChats
{
    public int Id { get; set; }

    public string Prompt { get; set; } = null!;

    public string Response { get; set; } = null!;

    public string SessionId { get; set; } = null!;

    public bool Include { get; set; } = true;

    public string? ConnectionInformation { get; set; }

    public int EmbedId { get; set; }

    public int? UsersId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual EmbedConfigs EmbedConfig { get; set; } = null!;

    public virtual Users? Users { get; set; }
}
