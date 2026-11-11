#nullable enable

using System;
using System.Collections.Generic;

namespace LiteLLM.Models;

public partial class LiteLLMLens
{
    public virtual ICollection<LiteLLMLensReview> Reviews { get; set; } = new List<LiteLLMLensReview>();

    public string Id { get; set; } = null!;

    public int Version { get; set; } = 0;

    public string Data { get; set; } = null!;

    public DateTime? DueAt { get; set; }
}
