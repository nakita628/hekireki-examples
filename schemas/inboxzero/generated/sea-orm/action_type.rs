use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "ActionType")]
pub enum ActionType {
    #[sea_orm(string_value = "ARCHIVE")]
    Archive,
    #[sea_orm(string_value = "LABEL")]
    Label,
    #[sea_orm(string_value = "REPLY")]
    Reply,
    #[sea_orm(string_value = "SEND_EMAIL")]
    SendEmail,
    #[sea_orm(string_value = "FORWARD")]
    Forward,
    #[sea_orm(string_value = "DRAFT_EMAIL")]
    DraftEmail,
    #[sea_orm(string_value = "DRAFT_MESSAGING_CHANNEL")]
    DraftMessagingChannel,
    #[sea_orm(string_value = "NOTIFY_MESSAGING_CHANNEL")]
    NotifyMessagingChannel,
    #[sea_orm(string_value = "MARK_SPAM")]
    MarkSpam,
    #[sea_orm(string_value = "CALL_WEBHOOK")]
    CallWebhook,
    #[sea_orm(string_value = "MARK_READ")]
    MarkRead,
    #[sea_orm(string_value = "STAR")]
    Star,
    #[sea_orm(string_value = "DELETE")]
    Delete,
    #[sea_orm(string_value = "DIGEST")]
    Digest,
    #[sea_orm(string_value = "MOVE_FOLDER")]
    MoveFolder,
    #[sea_orm(string_value = "NOTIFY_SENDER")]
    NotifySender,
    #[sea_orm(string_value = "INTEGRATION")]
    Integration,
}
