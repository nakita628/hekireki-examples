#nullable enable

using System;
using System.Collections.Generic;

namespace WorkoutCool.Models;

public partial class UserProgramEnrollment
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string ProgramId { get; set; } = null!;

    public DateTime EnrolledAt { get; set; }

    public int CurrentWeek { get; set; } = 1;

    public int CurrentSession { get; set; } = 1;

    public int CompletedSessions { get; set; } = 0;

    public bool IsActive { get; set; } = true;

    public DateTime? CompletedAt { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual Program Program { get; set; } = null!;

    public virtual ICollection<UserSessionProgress> SessionProgress { get; set; } = new List<UserSessionProgress>();
}
