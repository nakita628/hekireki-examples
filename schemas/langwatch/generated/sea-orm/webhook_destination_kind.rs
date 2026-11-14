use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "WebhookDestinationKind"
)]
pub enum WebhookDestinationKind {
    #[sea_orm(string_value = "http")]
    Http,
    #[sea_orm(string_value = "sqs")]
    Sqs,
}
