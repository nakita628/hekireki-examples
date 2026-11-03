#nullable enable

using System;

namespace Teable.Models;

public partial class Reference
{
    public string Id { get; set; } = null!;

    public string FromFieldId { get; set; } = null!;

    public string ToFieldId { get; set; } = null!;

    public DateTime CreatedTime { get; set; }
}
