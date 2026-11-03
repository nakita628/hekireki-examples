#nullable enable

namespace Postiz.Models;

public partial class SocialMediaAgencyNiche
{
    public string AgencyId { get; set; } = null!;

    public string Niche { get; set; } = null!;

    public virtual SocialMediaAgency Agency { get; set; } = null!;
}
