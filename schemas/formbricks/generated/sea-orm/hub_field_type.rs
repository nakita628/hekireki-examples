use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "HubFieldType")]
pub enum HubFieldType {
    #[sea_orm(string_value = "text")]
    Text,
    #[sea_orm(string_value = "categorical")]
    Categorical,
    #[sea_orm(string_value = "nps")]
    Nps,
    #[sea_orm(string_value = "csat")]
    Csat,
    #[sea_orm(string_value = "ces")]
    Ces,
    #[sea_orm(string_value = "rating")]
    Rating,
    #[sea_orm(string_value = "number")]
    Number,
    #[sea_orm(string_value = "boolean")]
    Boolean,
    #[sea_orm(string_value = "date")]
    Date,
}
