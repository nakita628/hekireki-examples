#nullable enable

using System;

namespace Calcom.Models;

public partial class ReminderMail
{
    public int Id { get; set; }

    public int ReferenceId { get; set; }

    public ReminderType ReminderType { get; set; }

    public int ElapsedMinutes { get; set; }

    public DateTime CreatedAt { get; set; }
}
