use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "AuthzedProjectionOutbox")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "targetType")]
    pub target_type: String,
    #[sea_orm(column_name = "primaryId")]
    pub primary_id: String,
    #[sea_orm(column_name = "secondaryId")]
    pub secondary_id: Option<String>,
    #[sea_orm(column_name = "isRevocation", default_value = false)]
    pub is_revocation: bool,
    #[sea_orm(default_value = 0)]
    pub attempts: i32,
    #[sea_orm(column_name = "permanentFailures", default_value = 0)]
    pub permanent_failures: i32,
    #[sea_orm(column_name = "availableAt")]
    pub available_at: DateTime,
    #[sea_orm(column_name = "leasedAt")]
    pub leased_at: Option<DateTime>,
    #[sea_orm(column_name = "leaseExpiresAt")]
    pub lease_expires_at: Option<DateTime>,
    #[sea_orm(column_name = "leaseOwner")]
    pub lease_owner: Option<String>,
    #[sea_orm(column_name = "processedAt")]
    pub processed_at: Option<DateTime>,
    #[sea_orm(column_name = "deadLetteredAt")]
    pub dead_lettered_at: Option<DateTime>,
    #[sea_orm(column_name = "lastAttemptAt")]
    pub last_attempt_at: Option<DateTime>,
    #[sea_orm(column_name = "lastErrorCode")]
    pub last_error_code: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    fn new() -> Self {
        Self {
            id: Set(uuid::Uuid::new_v4().to_string()),
            ..ActiveModelTrait::default()
        }
    }

    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.available_at.is_not_set() {
            self.available_at = Set(now.naive_utc());
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
