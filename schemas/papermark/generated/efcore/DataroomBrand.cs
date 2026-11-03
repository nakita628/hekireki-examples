#nullable enable

using System;

namespace Papermark.Models;

public partial class DataroomBrand
{
    public string Id { get; set; } = null!;

    public string? Logo { get; set; }

    public bool? HideLogo { get; set; }

    public string? Banner { get; set; }

    public string? BrandColor { get; set; }

    public string? AccentColor { get; set; }

    public string? AccentButtonColor { get; set; }

    public bool ApplyAccentColorToDataroomView { get; set; } = false;

    public string? WelcomeMessage { get; set; }

    public string CardLayout { get; set; } = "LIST";

    public bool ShowFolderTree { get; set; } = true;

    public string ViewerLayoutPreset { get; set; } = "STANDARD";

    public string ViewerHeaderStyle { get; set; } = "DEFAULT";

    public bool HideFolderIconsInMain { get; set; } = false;

    public string? CtaLabel { get; set; }

    public string? CtaUrl { get; set; }

    public bool CustomLinkPreviewEnabled { get; set; } = false;

    public string? LinkPreviewTitle { get; set; }

    public string? LinkPreviewDescription { get; set; }

    public string? LinkPreviewImage { get; set; }

    public string? LinkPreviewFavicon { get; set; }

    public string DefaultLanguage { get; set; } = "en";

    public string DataroomId { get; set; } = null!;

    public virtual Dataroom Dataroom { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
