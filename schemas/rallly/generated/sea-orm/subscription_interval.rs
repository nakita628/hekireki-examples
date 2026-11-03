use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "subscription_interval"
)]
pub enum SubscriptionInterval {
    #[sea_orm(string_value = "month")]
    Month,
    #[sea_orm(string_value = "year")]
    Year,
}
