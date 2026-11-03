#nullable enable

namespace Formbricks.Models;

public partial class ResponseQuotaLink
{
    public virtual Response Response { get; set; } = null!;

    public string ResponseId { get; set; } = null!;

    public virtual SurveyQuota Quota { get; set; } = null!;

    public string QuotaId { get; set; } = null!;

    public ResponseQuotaLinkStatus Status { get; set; }
}
