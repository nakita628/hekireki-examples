use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "EmbeddedDataType")]
pub enum EmbeddedDataType {
    #[sea_orm(string_value = "string")]
    String,
    #[sea_orm(string_value = "number")]
    Number,
    #[sea_orm(string_value = "boolean")]
    Boolean,
    #[sea_orm(string_value = "date")]
    Date,
}
