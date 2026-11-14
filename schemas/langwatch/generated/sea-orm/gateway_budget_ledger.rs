use super::gateway_budget_ledger_status::GatewayBudgetLedgerStatus;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "GatewayBudgetLedger")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "budgetId")]
    pub budget_id: String,
    #[sea_orm(column_name = "virtualKeyId")]
    pub virtual_key_id: String,
    #[sea_orm(column_name = "modelProviderId")]
    pub model_provider_id: Option<String>,
    #[sea_orm(column_name = "providerKey")]
    pub provider_key: Option<String>,
    #[sea_orm(column_name = "gatewayRequestId")]
    pub gateway_request_id: String,
    #[sea_orm(column_name = "amountUsd", column_type = "Decimal(Some((18, 6)))")]
    pub amount_usd: Decimal,
    #[sea_orm(column_name = "tokensInput", default_value = 0)]
    pub tokens_input: i32,
    #[sea_orm(column_name = "tokensOutput", default_value = 0)]
    pub tokens_output: i32,
    #[sea_orm(column_name = "tokensCacheRead", default_value = 0)]
    pub tokens_cache_read: i32,
    #[sea_orm(column_name = "tokensCacheWrite", default_value = 0)]
    pub tokens_cache_write: i32,
    pub model: String,
    #[sea_orm(column_name = "durationMs")]
    pub duration_ms: Option<i32>,
    #[sea_orm(default_value = "SUCCESS")]
    pub status: GatewayBudgetLedgerStatus,
    #[sea_orm(column_name = "occurredAt")]
    pub occurred_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::gateway_budget::Entity",
        from = "Column::BudgetId",
        to = "super::gateway_budget::Column::Id",
        on_delete = "Cascade"
    )]
    Budget,
    #[sea_orm(
        belongs_to = "super::virtual_key::Entity",
        from = "Column::VirtualKeyId",
        to = "super::virtual_key::Column::Id",
        on_delete = "Cascade"
    )]
    VirtualKey,
}

impl Related<super::gateway_budget::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Budget.def()
    }
}

impl Related<super::virtual_key::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::VirtualKey.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.occurred_at.is_not_set() {
            self.occurred_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
