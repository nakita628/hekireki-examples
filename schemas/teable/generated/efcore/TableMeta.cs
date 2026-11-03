#nullable enable

using System;
using System.Collections.Generic;

namespace Teable.Models;

public partial class TableMeta
{
    public string Id { get; set; } = null!;

    public string BaseId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Icon { get; set; }

    public string DbTableName { get; set; } = null!;

    public string? DbViewName { get; set; }

    public string? SearchIndex { get; set; }

    public string? SearchIndexPolicy { get; set; }

    public string? QueryIndexPolicy { get; set; }

    public ProvisionState ProvisionState { get; set; } = ProvisionState.Ready;

    public int Version { get; set; }

    public double Order { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public DateTime? DeletedTime { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string? LastModifiedBy { get; set; }

    public virtual Base Base { get; set; } = null!;

    public virtual ICollection<Field> Fields { get; set; } = new List<Field>();

    public virtual ICollection<View> Views { get; set; } = new List<View>();

    public virtual ICollection<PluginPanel> PluginPanel { get; set; } = new List<PluginPanel>();

    public virtual ICollection<PluginContextMenu> PluginContextMenu { get; set; } = new List<PluginContextMenu>();
}
