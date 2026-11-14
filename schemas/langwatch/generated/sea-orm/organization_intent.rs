use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "OrganizationIntent")]
pub enum OrganizationIntent {
    #[sea_orm(string_value = "AGENT_GOVERNANCE")]
    AgentGovernance,
    #[sea_orm(string_value = "LLM_OPS")]
    LlmOps,
}
