#nullable enable

using System;

namespace Formbricks.Models;

public partial class Display
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Survey Survey { get; set; } = null!;

    public string SurveyId { get; set; } = null!;

    public virtual Contact? Contact { get; set; }

    public string? ContactId { get; set; }

    public virtual Response? Response { get; set; }

    public string? CreativeId { get; set; }
}
