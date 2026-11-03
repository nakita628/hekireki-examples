use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "TaskEventKind")]
pub enum TaskEventKind {
    #[sea_orm(string_value = "UNSPECIFIED")]
    Unspecified,
    #[sea_orm(string_value = "INTERNAL")]
    Internal,
    #[sea_orm(string_value = "SERVER")]
    Server,
    #[sea_orm(string_value = "CLIENT")]
    Client,
    #[sea_orm(string_value = "PRODUCER")]
    Producer,
    #[sea_orm(string_value = "CONSUMER")]
    Consumer,
    #[sea_orm(string_value = "UNRECOGNIZED")]
    Unrecognized,
    #[sea_orm(string_value = "LOG")]
    Log,
}
