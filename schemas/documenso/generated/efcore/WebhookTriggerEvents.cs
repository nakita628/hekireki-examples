#nullable enable

using NpgsqlTypes;

namespace Documenso.Models;

public enum WebhookTriggerEvents
{
    [PgName("DOCUMENT_CREATED")]
    DocumentCreated,

    [PgName("DOCUMENT_SENT")]
    DocumentSent,

    [PgName("DOCUMENT_OPENED")]
    DocumentOpened,

    [PgName("DOCUMENT_SIGNED")]
    DocumentSigned,

    [PgName("DOCUMENT_COMPLETED")]
    DocumentCompleted,

    [PgName("DOCUMENT_REJECTED")]
    DocumentRejected,

    [PgName("DOCUMENT_CANCELLED")]
    DocumentCancelled,

    [PgName("RECIPIENT_EXPIRED")]
    RecipientExpired,

    [PgName("DOCUMENT_RECIPIENT_COMPLETED")]
    DocumentRecipientCompleted,

    [PgName("DOCUMENT_REMINDER_SENT")]
    DocumentReminderSent,

    [PgName("TEMPLATE_CREATED")]
    TemplateCreated,

    [PgName("TEMPLATE_UPDATED")]
    TemplateUpdated,

    [PgName("TEMPLATE_DELETED")]
    TemplateDeleted,

    [PgName("TEMPLATE_USED")]
    TemplateUsed,
}
