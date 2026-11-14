#nullable enable

using System;

namespace LangWatch.Models;

public partial class NurturingUncopiedProject
{
    public string ProjectId { get; set; } = null!;

    public DateTime FirstSeenAt { get; set; }
}
