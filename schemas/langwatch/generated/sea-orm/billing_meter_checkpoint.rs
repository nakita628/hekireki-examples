use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "BillingMeterCheckpoint")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    #[sea_orm(column_name = "billingMonth")]
    pub billing_month: String,
    #[sea_orm(default_value = "langwatch_billable_events")]
    pub meter: String,
    #[sea_orm(column_name = "lastReportedTotal", default_value = 0)]
    pub last_reported_total: i32,
    #[sea_orm(column_name = "pendingReportedTotal")]
    pub pending_reported_total: Option<i32>,
    #[sea_orm(column_name = "consecutiveFailures", default_value = 0)]
    pub consecutive_failures: i32,
    #[sea_orm(column_name = "lastCountedEventId")]
    pub last_counted_event_id: Option<String>,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, _insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
