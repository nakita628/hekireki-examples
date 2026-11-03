use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "FeedbackSourceImportMode"
)]
pub enum FeedbackSourceImportMode {
    #[sea_orm(string_value = "completedOnly")]
    Completedonly,
    #[sea_orm(string_value = "all")]
    All,
}
