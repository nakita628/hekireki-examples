use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "CreditType")]
pub enum CreditType {
    #[sea_orm(string_value = "MONTHLY")]
    Monthly,
    #[sea_orm(string_value = "ADDITIONAL")]
    Additional,
}
