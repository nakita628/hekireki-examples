use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "DocumentFilingStatus"
)]
pub enum DocumentFilingStatus {
    #[sea_orm(string_value = "PROCESSING")]
    Processing,
    #[sea_orm(string_value = "PENDING")]
    Pending,
    #[sea_orm(string_value = "FILED")]
    Filed,
    #[sea_orm(string_value = "REJECTED")]
    Rejected,
    #[sea_orm(string_value = "ERROR")]
    Error,
    #[sea_orm(string_value = "PREVIEW")]
    Preview,
}
