use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "ColdEmailStatus")]
pub enum ColdEmailStatus {
    #[sea_orm(string_value = "AI_LABELED_COLD")]
    AiLabeledCold,
    #[sea_orm(string_value = "USER_REJECTED_COLD")]
    UserRejectedCold,
}
