#nullable enable

using System;
using System.Collections.Generic;

namespace TypeHero.Models;

public partial class User
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime? EmailVerified { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

    public string Bio { get; set; } = "";

    public virtual ICollection<UserLink> UserLinks { get; set; } = new List<UserLink>();

    public UserStatus Status { get; set; } = UserStatus.Active;

    public string? BanReason { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    public virtual ICollection<Challenge> Challenge { get; set; } = new List<Challenge>();

    public virtual ICollection<Vote> Vote { get; set; } = new List<Vote>();

    public virtual ICollection<Submission> Submission { get; set; } = new List<Submission>();

    public virtual ICollection<Bookmark> Bookmark { get; set; } = new List<Bookmark>();

    public virtual ICollection<SharedSolution> SharedSolution { get; set; } = new List<SharedSolution>();

    public virtual ICollection<Comment> Comment { get; set; } = new List<Comment>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual ICollection<Report> ReportsAbout { get; set; } = new List<Report>();

    public virtual ICollection<Report> Moderated { get; set; } = new List<Report>();

    public virtual ICollection<ImageUpload> ImageUpload { get; set; } = new List<ImageUpload>();

    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();

    public virtual ICollection<BetaTokens> BetaTokens { get; set; } = new List<BetaTokens>();

    public virtual ICollection<Donation> Donations { get; set; } = new List<Donation>();

    public virtual ICollection<ShortURL> ShortUrls { get; set; } = new List<ShortURL>();

    public virtual ICollection<Notification> ToUserNotification { get; set; } = new List<Notification>();

    public virtual ICollection<Notification> FromUserNotification { get; set; } = new List<Notification>();
}
