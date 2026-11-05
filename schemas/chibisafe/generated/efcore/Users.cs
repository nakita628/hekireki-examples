#nullable enable

using System;
using System.Collections.Generic;

namespace Chibisafe.Models;

public partial class Users
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool Enabled { get; set; } = true;

    public string StorageQuota { get; set; } = "0";

    public string? ApiKey { get; set; }

    public DateTime? PasswordEditedAt { get; set; }

    public DateTime? ApiKeyEditedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? EditedAt { get; set; }

    public virtual ICollection<Files> Files { get; set; } = new List<Files>();

    public virtual ICollection<Roles> Roles { get; set; } = new List<Roles>();

    public virtual ICollection<ShortenedLinks> Links { get; set; } = new List<ShortenedLinks>();
}
