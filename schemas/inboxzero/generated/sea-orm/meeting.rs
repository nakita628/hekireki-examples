use super::meeting_processing_status::MeetingProcessingStatus;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Meeting")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "calendarEventId")]
    pub calendar_event_id: String,
    #[sea_orm(column_name = "eventTitle")]
    pub event_title: String,
    #[sea_orm(column_name = "startTime")]
    pub start_time: DateTime,
    #[sea_orm(column_name = "endTime")]
    pub end_time: DateTime,
    pub attendees: Json,
    #[sea_orm(column_name = "organizerEmail")]
    pub organizer_email: Option<String>,
    #[sea_orm(column_name = "joinOverride")]
    pub join_override: Option<bool>,
    #[sea_orm(column_name = "processingStatus", default_value = "PENDING")]
    pub processing_status: MeetingProcessingStatus,
    #[sea_orm(column_name = "processingAttempts", default_value = 0)]
    pub processing_attempts: i32,
    pub summary: Option<Json>,
    #[sea_orm(column_name = "followUpDraftStartedAt")]
    pub follow_up_draft_started_at: Option<DateTime>,
    #[sea_orm(column_name = "followUpDraftId")]
    pub follow_up_draft_id: Option<String>,
    #[sea_orm(column_name = "recapSentAt")]
    pub recap_sent_at: Option<DateTime>,
    #[sea_orm(column_name = "recordingId")]
    pub recording_id: Option<String>,
    #[sea_orm(column_name = "emailAccountId")]
    pub email_account_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::meeting_recording::Entity",
        from = "Column::RecordingId",
        to = "super::meeting_recording::Column::Id",
        on_delete = "SetNull"
    )]
    Recording,
    #[sea_orm(
        belongs_to = "super::email_account::Entity",
        from = "Column::EmailAccountId",
        to = "super::email_account::Column::Id",
        on_delete = "Cascade"
    )]
    EmailAccount,
}

impl Related<super::meeting_recording::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Recording.def()
    }
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
