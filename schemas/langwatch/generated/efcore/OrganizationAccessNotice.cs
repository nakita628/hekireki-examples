#nullable enable

using System;

namespace LangWatch.Models;

public partial class OrganizationAccessNotice
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string Kind { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Name { get; set; }

    public string? Idp { get; set; }

    public string DayKey { get; set; } = null!;

    public bool IdpRefused { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime? MailedAt { get; set; }
}
