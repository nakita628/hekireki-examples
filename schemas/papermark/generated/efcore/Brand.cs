#nullable enable

using System;
using System.Collections.Generic;

namespace Papermark.Models;

public partial class Brand
{
    public string Id { get; set; } = null!;

    public string? Logo { get; set; }

    public bool HideLogo { get; set; } = false;

    public string? Banner { get; set; }

    public string? BrandColor { get; set; }

    public string? AccentColor { get; set; }

    public string? AccentButtonColor { get; set; }

    public bool ApplyAccentColorToDataroomView { get; set; } = false;

    public string? WelcomeMessage { get; set; }

    public string? CtaLabel { get; set; }

    public string? CtaUrl { get; set; }

    public string? PrivacyPolicyUrl { get; set; }

    public string CardLayout { get; set; } = "LIST";

    public bool ShowFolderTree { get; set; } = true;

    public string ViewerLayoutPreset { get; set; } = "STANDARD";

    public string ViewerHeaderStyle { get; set; } = "DEFAULT";

    public bool HideFolderIconsInMain { get; set; } = false;

    public bool CustomLinkPreviewEnabled { get; set; } = false;

    public string? LinkPreviewTitle { get; set; }

    public string? LinkPreviewDescription { get; set; }

    public string? LinkPreviewImage { get; set; }

    public string? LinkPreviewFavicon { get; set; }

    public string DefaultLanguage { get; set; } = "en";

    public string Name { get; set; } = "Default";

    public string TeamId { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public virtual ICollection<Team> DefaultForTeam { get; set; } = new List<Team>();

    public virtual ICollection<Link> Links { get; set; } = new List<Link>();

    public virtual ICollection<Dataroom> Datarooms { get; set; } = new List<Dataroom>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
