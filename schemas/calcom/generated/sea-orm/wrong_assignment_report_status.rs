use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "WrongAssignmentReportStatus"
)]
pub enum WrongAssignmentReportStatus {
    #[sea_orm(string_value = "PENDING")]
    Pending,
    #[sea_orm(string_value = "REVIEWED")]
    Reviewed,
    #[sea_orm(string_value = "RESOLVED")]
    Resolved,
    #[sea_orm(string_value = "DISMISSED")]
    Dismissed,
}
