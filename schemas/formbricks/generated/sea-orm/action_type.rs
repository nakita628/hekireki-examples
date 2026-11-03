use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "ActionType")]
pub enum ActionType {
    #[sea_orm(string_value = "code")]
    Code,
    #[sea_orm(string_value = "noCode")]
    Nocode,
}
