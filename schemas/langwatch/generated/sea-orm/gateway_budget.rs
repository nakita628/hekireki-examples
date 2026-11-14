use super::gateway_budget_breach_action::GatewayBudgetBreachAction;
use super::gateway_budget_scope_type::GatewayBudgetScopeType;
use super::gateway_budget_window::GatewayBudgetWindow;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "GatewayBudget")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    #[sea_orm(column_name = "scopeType")]
    pub scope_type: GatewayBudgetScopeType,
    #[sea_orm(column_name = "scopeId")]
    pub scope_id: String,
    #[sea_orm(column_name = "providerKey")]
    pub provider_key: Option<String>,
    pub name: String,
    pub description: Option<String>,
    pub window: GatewayBudgetWindow,
    #[sea_orm(column_name = "limitUsd", column_type = "Decimal(Some((18, 6)))")]
    pub limit_usd: Decimal,
    #[sea_orm(column_name = "onBreach", default_value = "BLOCK")]
    pub on_breach: GatewayBudgetBreachAction,
    pub timezone: Option<String>,
    #[sea_orm(column_name = "externalId")]
    pub external_id: Option<String>,
    #[sea_orm(default_value = "{}")]
    pub metadata: Json,
    #[sea_orm(
        column_name = "spentUsd",
        column_type = "Decimal(Some((18, 6)))",
        default_value = 0
    )]
    pub spent_usd: Decimal,
    #[sea_orm(column_name = "currentPeriodStartedAt")]
    pub current_period_started_at: DateTime,
    #[sea_orm(column_name = "resetsAt")]
    pub resets_at: DateTime,
    #[sea_orm(column_name = "lastResetAt")]
    pub last_reset_at: Option<DateTime>,
    #[sea_orm(column_name = "cycleAnchorAt")]
    pub cycle_anchor_at: Option<DateTime>,
    #[sea_orm(column_name = "archivedAt")]
    pub archived_at: Option<DateTime>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "createdById")]
    pub created_by_id: String,
    #[sea_orm(column_name = "managedByVirtualKeyId")]
    pub managed_by_virtual_key_id: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::CreatedById",
        to = "super::user::Column::Id"
    )]
    CreatedBy,
    #[sea_orm(has_many = "super::gateway_budget_ledger::Entity")]
    LedgerEntries,
    #[sea_orm(has_many = "super::gateway_budget_bucket_boundary::Entity")]
    BucketBoundaries,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CreatedBy.def()
    }
}

impl Related<super::gateway_budget_ledger::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::LedgerEntries.def()
    }
}

impl Related<super::gateway_budget_bucket_boundary::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BucketBoundaries.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.current_period_started_at.is_not_set() {
            self.current_period_started_at = Set(now.naive_utc());
        }
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.naive_utc());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
