use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "ProvisionState")]
pub enum ProvisionState {
    #[sea_orm(string_value = "pending")]
    Pending,
    #[sea_orm(string_value = "ready")]
    Ready,
    #[sea_orm(string_value = "error")]
    Error,
    #[sea_orm(string_value = "deleting")]
    Deleting,
}
