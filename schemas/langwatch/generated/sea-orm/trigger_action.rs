use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "TriggerAction")]
pub enum TriggerAction {
    #[sea_orm(string_value = "SEND_EMAIL")]
    SendEmail,
    #[sea_orm(string_value = "ADD_TO_DATASET")]
    AddToDataset,
    #[sea_orm(string_value = "ADD_TO_ANNOTATION_QUEUE")]
    AddToAnnotationQueue,
    #[sea_orm(string_value = "SEND_SLACK_MESSAGE")]
    SendSlackMessage,
    #[sea_orm(string_value = "SEND_WEBHOOK")]
    SendWebhook,
}
