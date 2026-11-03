use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "SeatChangeType")]
pub enum SeatChangeType {
    #[sea_orm(string_value = "ADDITION")]
    Addition,
    #[sea_orm(string_value = "REMOVAL")]
    Removal,
}
