#nullable enable

using System;

namespace Teable.Models;

public partial class Ops
{
    public string Id { get; set; } = null!;

    public string Collection { get; set; } = null!;

    public string DocId { get; set; } = null!;

    public string DocType { get; set; } = null!;

    public int Version { get; set; }

    public string Operation { get; set; } = null!;

    public DateTime CreatedTime { get; set; }

    public string CreatedBy { get; set; } = null!;
}
