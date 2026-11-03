use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "AlertOccurrenceSeen")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "alertId")]
    pub alert_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "sourceEventId")]
    pub source_event_id: String,
    #[sea_orm(column_name = "rowTime", column_type = "TimestampWithTimeZone")]
    pub row_time: DateTimeWithTimeZone,
    #[sea_orm(column_name = "seenAt", column_type = "TimestampWithTimeZone")]
    pub seen_at: DateTimeWithTimeZone,
    #[sea_orm(column_name = "expiresAt", column_type = "TimestampWithTimeZone")]
    pub expires_at: DateTimeWithTimeZone,
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
        if insert && self.seen_at.is_not_set() {
            self.seen_at = Set(now.fixed_offset());
        }
        Ok(self)
    }
}
