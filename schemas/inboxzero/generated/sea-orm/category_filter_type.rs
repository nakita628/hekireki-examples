use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "CategoryFilterType")]
pub enum CategoryFilterType {
    #[sea_orm(string_value = "INCLUDE")]
    Include,
    #[sea_orm(string_value = "EXCLUDE")]
    Exclude,
}
