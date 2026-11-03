use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "AttributeType")]
pub enum AttributeType {
    #[sea_orm(string_value = "TEXT")]
    Text,
    #[sea_orm(string_value = "NUMBER")]
    Number,
    #[sea_orm(string_value = "SINGLE_SELECT")]
    SingleSelect,
    #[sea_orm(string_value = "MULTI_SELECT")]
    MultiSelect,
}
