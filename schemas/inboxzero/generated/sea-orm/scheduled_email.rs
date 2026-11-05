use super::email_reminder_status::EmailReminderStatus;
use super::scheduled_email_status::ScheduledEmailStatus;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "ScheduledEmail")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "emailAccountId")]
    pub email_account_id: String,
    #[sea_orm(column_name = "clientMutationId")]
    pub client_mutation_id: String,
    #[sea_orm(column_name = "payloadHash")]
    pub payload_hash: String,
    pub payload: Json,
    #[sea_orm(column_name = "threadId")]
    pub thread_id: Option<String>,
    #[sea_orm(column_name = "sendAt")]
    pub send_at: DateTime,
    #[sea_orm(default_value = "PENDING")]
    pub status: ScheduledEmailStatus,
    #[sea_orm(column_name = "processingStartedAt")]
    pub processing_started_at: Option<DateTime>,
    #[sea_orm(column_name = "executionQueuedAt")]
    pub execution_queued_at: Option<DateTime>,
    #[sea_orm(column_name = "sentAt")]
    pub sent_at: Option<DateTime>,
    pub error: Option<String>,
    #[sea_orm(column_name = "remindAt")]
    pub remind_at: Option<DateTime>,
    #[sea_orm(column_name = "reminderStatus", default_value = "NONE")]
    pub reminder_status: EmailReminderStatus,
    #[sea_orm(column_name = "reminderStartedAt")]
    pub reminder_started_at: Option<DateTime>,
    #[sea_orm(column_name = "heldForUndo", default_value = false)]
    pub held_for_undo: bool,
    #[sea_orm(column_name = "draftMessageIds")]
    pub draft_message_ids: Vec<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::email_account::Entity",
        from = "Column::EmailAccountId",
        to = "super::email_account::Column::Id",
        on_delete = "Cascade"
    )]
    EmailAccount,
}

impl Related<super::email_account::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EmailAccount.def()
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
