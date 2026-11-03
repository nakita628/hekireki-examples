use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "mastra_knowledge_semantic_outbox")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "idempotencyKey")]
    pub idempotency_key: String,
    #[sea_orm(column_name = "documentId")]
    pub document_id: String,
    #[sea_orm(column_name = "documentType")]
    pub document_type: String,
    pub operation: String,
    pub scope: Json,
    #[sea_orm(column_name = "scopeKey")]
    pub scope_key: String,
    pub status: String,
    pub attempts: i32,
    #[sea_orm(column_name = "availableAt")]
    pub available_at: DateTime,
    #[sea_orm(column_name = "claimedAt")]
    pub claimed_at: Option<DateTime>,
    #[sea_orm(column_name = "claimedBy")]
    pub claimed_by: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "completedAt")]
    pub completed_at: Option<DateTime>,
    #[sea_orm(column_name = "availableAtZ", column_type = "TimestampWithTimeZone")]
    pub available_at_z: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_name = "claimedAtZ", column_type = "TimestampWithTimeZone")]
    pub claimed_at_z: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_name = "createdAtZ", column_type = "TimestampWithTimeZone")]
    pub created_at_z: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_name = "completedAtZ", column_type = "TimestampWithTimeZone")]
    pub completed_at_z: Option<DateTimeWithTimeZone>,
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
        if insert && self.available_at_z.is_not_set() {
            self.available_at_z = Set(Some(now.fixed_offset()));
        }
        if insert && self.claimed_at_z.is_not_set() {
            self.claimed_at_z = Set(Some(now.fixed_offset()));
        }
        if insert && self.created_at_z.is_not_set() {
            self.created_at_z = Set(Some(now.fixed_offset()));
        }
        if insert && self.completed_at_z.is_not_set() {
            self.completed_at_z = Set(Some(now.fixed_offset()));
        }
        Ok(self)
    }
}
