#nullable enable

using System;

namespace Rallly.Models;

public partial class InstanceSettings
{
    public int Id { get; set; } = 1;

    public bool DisableUserRegistration { get; set; } = false;

    public Guid InstanceId { get; set; }

    public string? AppName { get; set; }

    public string? PrimaryColor { get; set; }

    public string? PrimaryColorDark { get; set; }

    public string? Logo { get; set; }

    public string? LogoDark { get; set; }

    public string? LogoIcon { get; set; }

    public bool? HideAttribution { get; set; }

    public string FooterLinks { get; set; } = "[]";

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
