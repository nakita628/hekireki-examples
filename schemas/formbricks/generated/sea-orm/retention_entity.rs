use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "RetentionEntity")]
pub enum RetentionEntity {
    #[sea_orm(string_value = "responses")]
    Responses,
    #[sea_orm(string_value = "surveys")]
    Surveys,
    #[sea_orm(string_value = "members")]
    Members,
}
