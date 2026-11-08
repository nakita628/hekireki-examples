use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "EmailSourceType")]
pub enum EmailSourceType {
    #[sea_orm(string_value = "TRANSACTIONAL")]
    Transactional,
    #[sea_orm(string_value = "CAMPAIGN")]
    Campaign,
    #[sea_orm(string_value = "WORKFLOW")]
    Workflow,
    #[sea_orm(string_value = "INBOUND")]
    Inbound,
}
