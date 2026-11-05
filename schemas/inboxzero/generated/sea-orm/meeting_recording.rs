use super::meeting_recording_status::MeetingRecordingStatus;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "MeetingRecording")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "botProvider", default_value = "recall")]
    pub bot_provider: String,
    #[sea_orm(column_name = "externalBotId")]
    pub external_bot_id: Option<String>,
    #[sea_orm(column_name = "botName")]
    pub bot_name: Option<String>,
    #[sea_orm(column_name = "externalRecordingId")]
    pub external_recording_id: Option<String>,
    #[sea_orm(column_name = "transcriptRequestedAt")]
    pub transcript_requested_at: Option<DateTime>,
    #[sea_orm(column_name = "externalTranscriptId")]
    pub external_transcript_id: Option<String>,
    #[sea_orm(column_name = "meetingUrl")]
    pub meeting_url: String,
    #[sea_orm(column_name = "normalizedMeetingUrl")]
    pub normalized_meeting_url: String,
    #[sea_orm(column_name = "activeKey")]
    pub active_key: Option<String>,
    #[sea_orm(column_name = "meetingStartTime")]
    pub meeting_start_time: DateTime,
    #[sea_orm(default_value = "PENDING")]
    pub status: MeetingRecordingStatus,
    #[sea_orm(column_name = "failureReason")]
    pub failure_reason: Option<String>,
    pub transcript: Option<Json>,
    #[sea_orm(column_name = "transcriptFetchedAt")]
    pub transcript_fetched_at: Option<DateTime>,
    #[sea_orm(column_name = "mediaDeletedAt")]
    pub media_deleted_at: Option<DateTime>,
    #[sea_orm(column_name = "emailAccountId")]
    pub email_account_id: String,
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
    #[sea_orm(has_many = "super::meeting::Entity")]
    Meetings,
}

impl Related<super::email_account::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EmailAccount.def()
    }
}

impl Related<super::meeting::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Meetings.def()
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
