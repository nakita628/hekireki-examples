#nullable enable

using System;

namespace LangWatch.Models;

public partial class SsoCredential
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string ConnectionId { get; set; } = null!;

    public string Kind { get; set; } = null!;

    public string Ciphertext { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
