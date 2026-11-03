use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "AlertDelivery")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "alertEventId")]
    pub alert_event_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "eventAt", column_type = "TimestampWithTimeZone")]
    pub event_at: DateTimeWithTimeZone,
    #[sea_orm(column_name = "notifierId")]
    pub notifier_id: String,
    pub notifier: Json,
    #[sea_orm(default_value = "pending")]
    pub status: String,
    #[sea_orm(column_name = "claimId")]
    pub claim_id: Option<String>,
    #[sea_orm(column_name = "claimedAt", column_type = "TimestampWithTimeZone")]
    pub claimed_at: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_name = "deliveredAt", column_type = "TimestampWithTimeZone")]
    pub delivered_at: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_name = "failureReason")]
    pub failure_reason: Option<String>,
    #[sea_orm(column_name = "providerMetadata")]
    pub provider_metadata: Option<Json>,
    #[sea_orm(default_value = 0)]
    pub attempts: i32,
    #[sea_orm(column_name = "firstAttemptAt", column_type = "TimestampWithTimeZone")]
    pub first_attempt_at: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_name = "createdAt", column_type = "TimestampWithTimeZone")]
    pub created_at: DateTimeWithTimeZone,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.fixed_offset());
        }
        Ok(self)
    }
}
