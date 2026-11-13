use super::bulk_inbox_action_job_status::BulkInboxActionJobStatus;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "BulkInboxActionJob")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(default_value = "RUNNING")]
    pub status: BulkInboxActionJobStatus,
    pub search: Json,
    pub action: String,
    #[sea_orm(column_name = "labelId")]
    pub label_id: Option<String>,
    #[sea_orm(column_name = "labelName")]
    pub label_name: Option<String>,
    pub instruction: Option<String>,
    #[sea_orm(column_name = "pageToken")]
    pub page_token: Option<String>,
    #[sea_orm(column_name = "rateLimitedUntil")]
    pub rate_limited_until: Option<DateTime>,
    #[sea_orm(column_name = "leaseExpiresAt")]
    pub lease_expires_at: Option<DateTime>,
    #[sea_orm(column_name = "chunkToken")]
    pub chunk_token: Option<String>,
    #[sea_orm(column_name = "recoveryCount", default_value = 0)]
    pub recovery_count: i32,
    #[sea_orm(column_name = "processedCount", default_value = 0)]
    pub processed_count: i32,
    #[sea_orm(column_name = "matchedCount", default_value = 0)]
    pub matched_count: i32,
    #[sea_orm(column_name = "actedCount", default_value = 0)]
    pub acted_count: i32,
    #[sea_orm(column_name = "skippedCount", default_value = 0)]
    pub skipped_count: i32,
    #[sea_orm(column_name = "failedCount", default_value = 0)]
    pub failed_count: i32,
    #[sea_orm(column_name = "limitReached", default_value = false)]
    pub limit_reached: bool,
    pub error: Option<String>,
    #[sea_orm(column_name = "completedAt")]
    pub completed_at: Option<DateTime>,
    #[sea_orm(column_name = "confirmationKey")]
    pub confirmation_key: String,
    #[sea_orm(column_name = "chatId")]
    pub chat_id: Option<String>,
    #[sea_orm(column_name = "emailAccountId")]
    pub email_account_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::chat::Entity",
        from = "Column::ChatId",
        to = "super::chat::Column::Id",
        on_delete = "SetNull"
    )]
    Chat,
    #[sea_orm(
        belongs_to = "super::email_account::Entity",
        from = "Column::EmailAccountId",
        to = "super::email_account::Column::Id",
        on_delete = "Cascade"
    )]
    EmailAccount,
    #[sea_orm(has_many = "super::bulk_inbox_action_thread::Entity")]
    HandledThreads,
}

impl Related<super::chat::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Chat.def()
    }
}

impl Related<super::email_account::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EmailAccount.def()
    }
}

impl Related<super::bulk_inbox_action_thread::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::HandledThreads.def()
    }
}

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
