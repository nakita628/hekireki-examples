use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "GatewayBudgetLedgerStatus"
)]
pub enum GatewayBudgetLedgerStatus {
    #[sea_orm(string_value = "SUCCESS")]
    Success,
    #[sea_orm(string_value = "PROVIDER_ERROR")]
    ProviderError,
    #[sea_orm(string_value = "BLOCKED_BY_GUARDRAIL")]
    BlockedByGuardrail,
    #[sea_orm(string_value = "CANCELLED")]
    Cancelled,
}
