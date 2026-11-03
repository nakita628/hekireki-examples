use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "CalVideoSettings")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "eventTypeId")]
    pub event_type_id: i32,
    #[sea_orm(column_name = "disableRecordingForOrganizer", default_value = false)]
    pub disable_recording_for_organizer: bool,
    #[sea_orm(column_name = "disableRecordingForGuests", default_value = false)]
    pub disable_recording_for_guests: bool,
    #[sea_orm(column_name = "enableAutomaticTranscription", default_value = false)]
    pub enable_automatic_transcription: bool,
    #[sea_orm(
        column_name = "enableAutomaticRecordingForOrganizer",
        default_value = false
    )]
    pub enable_automatic_recording_for_organizer: bool,
    #[sea_orm(column_name = "redirectUrlOnExit")]
    pub redirect_url_on_exit: Option<String>,
    #[sea_orm(column_name = "disableTranscriptionForGuests", default_value = false)]
    pub disable_transcription_for_guests: bool,
    #[sea_orm(
        column_name = "disableTranscriptionForOrganizer",
        default_value = false
    )]
    pub disable_transcription_for_organizer: bool,
    #[sea_orm(column_name = "requireEmailForGuests", default_value = false)]
    pub require_email_for_guests: bool,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::event_type::Entity",
        from = "Column::EventTypeId",
        to = "super::event_type::Column::Id",
        on_delete = "Cascade"
    )]
    EventType,
}

impl Related<super::event_type::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EventType.def()
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
