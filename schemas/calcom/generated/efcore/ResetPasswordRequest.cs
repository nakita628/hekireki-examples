#nullable enable

using System;

namespace Calcom.Models;

public partial class ResetPasswordRequest
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Email { get; set; } = null!;

    public DateTime Expires { get; set; }
}
