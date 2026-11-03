#nullable enable

namespace Documenso.Models;

public partial class DocumentMeta
{
    public string Id { get; set; } = null!;

    public string? Subject { get; set; }

    public string? Message { get; set; }

    public string? Timezone { get; set; } = "Etc/UTC";

    public string? DateFormat { get; set; } = "yyyy-MM-dd hh:mm a";

    public string? RedirectUrl { get; set; }

    public DocumentSigningOrder SigningOrder { get; set; } = DocumentSigningOrder.Parallel;

    public bool AllowDictateNextSigner { get; set; } = false;

    public bool TypedSignatureEnabled { get; set; } = true;

    public bool UploadSignatureEnabled { get; set; } = true;

    public bool DrawSignatureEnabled { get; set; } = true;

    public string Language { get; set; } = "en";

    public DocumentDistributionMethod DistributionMethod { get; set; } = DocumentDistributionMethod.Email;

    public string? EmailSettings { get; set; }

    public string? EmailReplyTo { get; set; }

    public string? EmailId { get; set; }

    public string? EnvelopeExpirationPeriod { get; set; }

    public string? ReminderSettings { get; set; }

    public virtual Envelope? Envelope { get; set; }
}
