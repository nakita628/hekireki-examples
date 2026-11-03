#nullable enable

using System;
using System.Collections.Generic;

namespace Papermark.Models;

public partial class LinkPreset
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string TeamId { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public string? PId { get; set; }

    public bool? EnableCustomMetaTag { get; set; } = false;

    public string? MetaTitle { get; set; }

    public string? MetaDescription { get; set; }

    public string? MetaImage { get; set; }

    public string? MetaFavicon { get; set; }

    public bool? EnableNotification { get; set; } = false;

    public bool? EmailProtected { get; set; } = true;

    public bool? EmailAuthenticated { get; set; } = false;

    public bool? AllowDownload { get; set; } = false;

    public bool? EnableAllowList { get; set; } = false;

    public List<string>? AllowList { get; set; }

    public bool? EnableDenyList { get; set; } = false;

    public List<string>? DenyList { get; set; }

    public int? ExpiresIn { get; set; }

    public bool? EnableScreenshotProtection { get; set; } = false;

    public bool? EnableConfidentialView { get; set; } = false;

    public DateTime? ExpiresAt { get; set; }

    public bool? EnablePassword { get; set; } = false;

    public string? Password { get; set; }

    public bool? EnableWatermark { get; set; } = false;

    public string? WatermarkConfig { get; set; }

    public bool? EnableAgreement { get; set; } = false;

    public string? AgreementId { get; set; }

    public bool? EnableCustomFields { get; set; } = false;

    public string? CustomFields { get; set; }

    public bool? ShowBanner { get; set; } = false;

    public string? WelcomeMessage { get; set; }

    public bool IsDefault { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
