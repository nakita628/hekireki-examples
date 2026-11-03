use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "SpaceDataDbBindingState"
)]
pub enum SpaceDataDbBindingState {
    #[sea_orm(string_value = "ready")]
    Ready,
    #[sea_orm(string_value = "validating")]
    Validating,
    #[sea_orm(string_value = "initializing")]
    Initializing,
    #[sea_orm(string_value = "migrating")]
    Migrating,
    #[sea_orm(string_value = "error")]
    Error,
    #[sea_orm(string_value = "disabled")]
    Disabled,
}
