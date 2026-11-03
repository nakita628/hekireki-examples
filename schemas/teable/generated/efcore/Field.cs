#nullable enable

using System;

namespace Teable.Models;

public partial class Field
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Options { get; set; }

    public string? Meta { get; set; }

    public string? AiConfig { get; set; }

    public string Type { get; set; } = null!;

    public string CellValueType { get; set; } = null!;

    public bool? IsMultipleCellValue { get; set; }

    public string DbFieldType { get; set; } = null!;

    public string DbFieldName { get; set; } = null!;

    public ProvisionState ProvisionState { get; set; } = ProvisionState.Ready;

    public bool? NotNull { get; set; }

    public bool? Unique { get; set; }

    public bool? IsPrimary { get; set; }

    public bool? IsComputed { get; set; }

    public bool? IsLookup { get; set; }

    public bool? IsConditionalLookup { get; set; }

    public bool? IsPending { get; set; }

    public bool? HasError { get; set; }

    public string? LookupLinkedFieldId { get; set; }

    public string? LookupOptions { get; set; }

    public string TableId { get; set; } = null!;

    public double Order { get; set; }

    public int Version { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public DateTime? DeletedTime { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string? LastModifiedBy { get; set; }

    public virtual TableMeta Table { get; set; } = null!;
}
