#nullable enable

using System;

namespace WorkoutCool.Models;

public partial class UserSessionProgress
{
    public string Id { get; set; } = null!;

    public string EnrollmentId { get; set; } = null!;

    public string SessionId { get; set; } = null!;

    public DateTime StartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public string? WorkoutSessionId { get; set; }

    public virtual UserProgramEnrollment Enrollment { get; set; } = null!;

    public virtual ProgramSession Session { get; set; } = null!;

    public virtual WorkoutSession? WorkoutSession { get; set; }
}
