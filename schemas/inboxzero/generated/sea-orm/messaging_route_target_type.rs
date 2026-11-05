use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "MessagingRouteTargetType"
)]
pub enum MessagingRouteTargetType {
    #[sea_orm(string_value = "CHANNEL")]
    Channel,
    #[sea_orm(string_value = "DIRECT_MESSAGE")]
    DirectMessage,
}
