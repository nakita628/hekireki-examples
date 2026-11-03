use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "Period")]
pub enum Period {
    #[sea_orm(string_value = "MONTHLY")]
    Monthly,
    #[sea_orm(string_value = "YEARLY")]
    Yearly,
}
