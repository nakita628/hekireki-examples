use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "EventTypeCustomInputType"
)]
pub enum EventTypeCustomInputType {
    #[sea_orm(string_value = "text")]
    Text,
    #[sea_orm(string_value = "textLong")]
    Textlong,
    #[sea_orm(string_value = "number")]
    Number,
    #[sea_orm(string_value = "bool")]
    Bool,
    #[sea_orm(string_value = "radio")]
    Radio,
    #[sea_orm(string_value = "phone")]
    Phone,
}
