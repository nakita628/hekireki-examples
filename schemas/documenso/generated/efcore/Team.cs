#nullable enable

using System;
using System.Collections.Generic;

namespace Documenso.Models;

public partial class Team
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Url { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? AvatarImageId { get; set; }

    public virtual TeamEmail? TeamEmail { get; set; }

    public virtual TeamEmailVerification? EmailVerification { get; set; }

    public virtual AvatarImage? AvatarImage { get; set; }

    public virtual TeamProfile? Profile { get; set; }

    public virtual ICollection<Envelope> Envelopes { get; set; } = new List<Envelope>();

    public virtual ICollection<Folder> Folders { get; set; } = new List<Folder>();

    public virtual ICollection<ApiToken> ApiTokens { get; set; } = new List<ApiToken>();

    public virtual ICollection<Webhook> Webhooks { get; set; } = new List<Webhook>();

    public virtual ICollection<TeamGroup> TeamGroups { get; set; } = new List<TeamGroup>();

    public string OrganisationId { get; set; } = null!;

    public virtual Organisation Organisation { get; set; } = null!;

    public string TeamGlobalSettingsId { get; set; } = null!;

    public virtual TeamGlobalSettings TeamGlobalSettings { get; set; } = null!;
}
