#nullable enable

using System;
using System.Collections.Generic;

namespace Calcom.Models;

public partial class UserHolidaySettings
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;

    public string? CountryCode { get; set; }

    public List<string>? DisabledIds { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
