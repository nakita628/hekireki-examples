use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "LogicalOperator")]
pub enum LogicalOperator {
    #[sea_orm(string_value = "AND")]
    And,
    #[sea_orm(string_value = "OR")]
    Or,
}
