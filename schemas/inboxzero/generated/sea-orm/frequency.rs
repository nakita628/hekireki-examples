use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "Frequency")]
pub enum Frequency {
    #[sea_orm(string_value = "NEVER")]
    Never,
    #[sea_orm(string_value = "DAILY")]
    Daily,
    #[sea_orm(string_value = "WEEKLY")]
    Weekly,
}
