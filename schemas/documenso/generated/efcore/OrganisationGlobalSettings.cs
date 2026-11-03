#nullable enable

namespace Documenso.Models;

public partial class OrganisationGlobalSettings
{
    public string Id { get; set; } = null!;

    public virtual Organisation? Organisation { get; set; }

    public DocumentVisibility DocumentVisibility { get; set; } = DocumentVisibility.Everyone;

    public string DocumentLanguage { get; set; } = "en";

    public bool IncludeSenderDetails { get; set; } = true;

    public bool IncludeSigningCertificate { get; set; } = true;

    public bool IncludeAuditLog { get; set; } = false;

    public string? DocumentTimezone { get; set; }

    public string DocumentDateFormat { get; set; } = "yyyy-MM-dd hh:mm a";

    public bool DelegateDocumentOwnership { get; set; } = false;

    public bool TypedSignatureEnabled { get; set; } = true;

    public bool UploadSignatureEnabled { get; set; } = true;

    public bool DrawSignatureEnabled { get; set; } = true;

    public string? DefaultRecipients { get; set; }

    public string? EmailId { get; set; }

    public virtual OrganisationEmail? Email { get; set; }

    public string? EmailReplyTo { get; set; }

    public string EmailDocumentSettings { get; set; } = null!;

    public bool BrandingEnabled { get; set; } = false;

    public string BrandingLogo { get; set; } = "";

    public string BrandingUrl { get; set; } = "";

    public string BrandingCompanyDetails { get; set; } = "";

    public string? BrandingColors { get; set; }

    public string BrandingCss { get; set; } = "";

    public string? EnvelopeExpirationPeriod { get; set; }

    public string? ReminderSettings { get; set; }

    public bool AiFeaturesEnabled { get; set; } = false;
}
