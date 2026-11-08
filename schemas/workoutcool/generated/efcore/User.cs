#nullable enable

using System;
using System.Collections.Generic;

namespace WorkoutCool.Models;

public partial class User
{
    public string Id { get; set; } = null!;

    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool EmailVerified { get; set; }

    public string? Image { get; set; }

    public string? Locale { get; set; } = "fr";

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<Feedbacks> Feedbacks { get; set; } = new List<Feedbacks>();

    public UserRole? Role { get; set; } = UserRole.User;

    public bool? Banned { get; set; } = false;

    public string? BanReason { get; set; }

    public DateTime? BanExpires { get; set; }

    public virtual ICollection<WorkoutSession> WorkoutSession { get; set; } = new List<WorkoutSession>();

    public virtual ICollection<UserFavoriteExercise> FavoriteExercises { get; set; } = new List<UserFavoriteExercise>();

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public virtual ICollection<License> Licenses { get; set; } = new List<License>();

    public bool? IsPremium { get; set; } = false;

    public string? OnboardingPreferences { get; set; }

    public virtual ICollection<UserProgramEnrollment> ProgramEnrollments { get; set; } = new List<UserProgramEnrollment>();
}
