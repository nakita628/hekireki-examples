use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "State")]
pub enum State {
    #[sea_orm(string_value = "QUEUE")]
    Queue,
    #[sea_orm(string_value = "PUBLISHED")]
    Published,
    #[sea_orm(string_value = "ERROR")]
    Error,
    #[sea_orm(string_value = "DRAFT")]
    Draft,
}
