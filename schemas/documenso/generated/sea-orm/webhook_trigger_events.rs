use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "WebhookTriggerEvents"
)]
pub enum WebhookTriggerEvents {
    #[sea_orm(string_value = "DOCUMENT_CREATED")]
    DocumentCreated,
    #[sea_orm(string_value = "DOCUMENT_SENT")]
    DocumentSent,
    #[sea_orm(string_value = "DOCUMENT_OPENED")]
    DocumentOpened,
    #[sea_orm(string_value = "DOCUMENT_SIGNED")]
    DocumentSigned,
    #[sea_orm(string_value = "DOCUMENT_COMPLETED")]
    DocumentCompleted,
    #[sea_orm(string_value = "DOCUMENT_REJECTED")]
    DocumentRejected,
    #[sea_orm(string_value = "DOCUMENT_CANCELLED")]
    DocumentCancelled,
    #[sea_orm(string_value = "RECIPIENT_EXPIRED")]
    RecipientExpired,
    #[sea_orm(string_value = "DOCUMENT_RECIPIENT_COMPLETED")]
    DocumentRecipientCompleted,
    #[sea_orm(string_value = "DOCUMENT_REMINDER_SENT")]
    DocumentReminderSent,
    #[sea_orm(string_value = "TEMPLATE_CREATED")]
    TemplateCreated,
    #[sea_orm(string_value = "TEMPLATE_UPDATED")]
    TemplateUpdated,
    #[sea_orm(string_value = "TEMPLATE_DELETED")]
    TemplateDeleted,
    #[sea_orm(string_value = "TEMPLATE_USED")]
    TemplateUsed,
}
