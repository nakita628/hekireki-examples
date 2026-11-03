use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "ShortLinkPreference"
)]
pub enum ShortLinkPreference {
    #[sea_orm(string_value = "ASK")]
    Ask,
    #[sea_orm(string_value = "YES")]
    Yes,
    #[sea_orm(string_value = "NO")]
    No,
}
