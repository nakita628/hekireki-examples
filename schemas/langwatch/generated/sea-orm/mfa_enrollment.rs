use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "MfaEnrollment")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "userId")]
    pub user_id: String,
    #[sea_orm(column_name = "enrollmentId")]
    pub enrollment_id: Option<String>,
    pub method: Option<String>,
    pub state: String,
    #[sea_orm(column_name = "enrolledAt")]
    pub enrolled_at: Option<DateTime>,
    #[sea_orm(column_name = "confirmedAt")]
    pub confirmed_at: Option<DateTime>,
    #[sea_orm(column_name = "expiredAt")]
    pub expired_at: Option<DateTime>,
    #[sea_orm(column_name = "disabledAt")]
    pub disabled_at: Option<DateTime>,
    #[sea_orm(column_name = "disabledVia")]
    pub disabled_via: Option<String>,
    #[sea_orm(column_name = "backupCodeCount", default_value = 0)]
    pub backup_code_count: i32,
    #[sea_orm(column_name = "consumedBackupCodeIndexes")]
    pub consumed_backup_code_indexes: Vec<i32>,
    #[sea_orm(column_name = "failedCount", default_value = 0)]
    pub failed_count: i32,
    #[sea_orm(column_name = "occurredAt")]
    pub occurred_at: DateTime,
    #[sea_orm(column_name = "lastEventId")]
    pub last_event_id: String,
    #[sea_orm(column_name = "acceptedAt")]
    pub accepted_at: DateTime,
    #[sea_orm(column_name = "projectionVersion")]
    pub projection_version: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
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
            self.created_at = Set(now.naive_utc());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
