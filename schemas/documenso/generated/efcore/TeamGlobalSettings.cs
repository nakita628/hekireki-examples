#nullable enable

namespace Documenso.Models;

public partial class TeamGlobalSettings
{
    public string Id { get; set; } = null!;

    public virtual Team? Team { get; set; }

    public DocumentVisibility? DocumentVisibility { get; set; }

    public string? DocumentLanguage { get; set; }

    public string? DocumentTimezone { get; set; }

    public string? DocumentDateFormat { get; set; }

    public bool? DelegateDocumentOwnership { get; set; }

    public bool? IncludeSenderDetails { get; set; }

    public bool? IncludeSigningCertificate { get; set; }

    public bool? IncludeAuditLog { get; set; }

    public bool? TypedSignatureEnabled { get; set; }

    public bool? UploadSignatureEnabled { get; set; }

    public bool? DrawSignatureEnabled { get; set; }

    public string? DefaultRecipients { get; set; }

    public string? EmailId { get; set; }

    public virtual OrganisationEmail? Email { get; set; }

    public string? EmailReplyTo { get; set; }

    public string? EmailDocumentSettings { get; set; }

    public bool? BrandingEnabled { get; set; }

    public string? BrandingLogo { get; set; }

    public string? BrandingUrl { get; set; }

    public string? BrandingCompanyDetails { get; set; }

    public string? BrandingColors { get; set; }

    public string? BrandingCss { get; set; }

    public string? EnvelopeExpirationPeriod { get; set; }

    public string? ReminderSettings { get; set; }

    public bool? AiFeaturesEnabled { get; set; }
}
