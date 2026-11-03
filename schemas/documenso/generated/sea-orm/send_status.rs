use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "SendStatus")]
pub enum SendStatus {
    #[sea_orm(string_value = "NOT_SENT")]
    NotSent,
    #[sea_orm(string_value = "SENT")]
    Sent,
}
