use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "MobilePushTokenType"
)]
pub enum MobilePushTokenType {
    #[sea_orm(string_value = "expo")]
    Expo,
    #[sea_orm(string_value = "apns")]
    Apns,
    #[sea_orm(string_value = "fcm")]
    Fcm,
}
