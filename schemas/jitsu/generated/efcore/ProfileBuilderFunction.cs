#nullable enable

using System;

namespace Jitsu.Models;

public partial class ProfileBuilderFunction
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string ProfileBuilderId { get; set; } = null!;

    public virtual ProfileBuilder ProfileBuilder { get; set; } = null!;

    public string FunctionId { get; set; } = null!;

    public virtual ConfigurationObject Function { get; set; } = null!;
}
