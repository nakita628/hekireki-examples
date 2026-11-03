#nullable enable

using System;
using System.Collections.Generic;

namespace Teable.Models;

public partial class Plugin
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? DetailDesc { get; set; }

    public string Logo { get; set; } = null!;

    public string? HelpUrl { get; set; }

    public string Status { get; set; } = null!;

    public string Positions { get; set; } = null!;

    public string? Url { get; set; }

    public string Secret { get; set; } = null!;

    public string MaskedSecret { get; set; } = null!;

    public string? I18n { get; set; }

    public string? Config { get; set; }

    public string? PluginUser { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string? LastModifiedBy { get; set; }

    public virtual ICollection<PluginInstall> PluginInstall { get; set; } = new List<PluginInstall>();
}
