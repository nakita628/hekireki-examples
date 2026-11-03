use super::poll_closed_reason::PollClosedReason;
use super::poll_kind::PollKind;
use super::poll_status::PollStatus;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "polls")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub created_at: DateTime,
    pub updated_at: DateTime,
    pub deadline: Option<DateTime>,
    pub title: String,
    pub description: Option<String>,
    pub location: Option<String>,
    pub conferencing: Option<Json>,
    pub user_id: Option<String>,
    pub time_zone: Option<String>,
    #[sea_orm(default_value = "open")]
    pub status: PollStatus,
    pub closed_reason: Option<PollClosedReason>,
    #[sea_orm(default_value = "date")]
    pub kind: PollKind,
    #[sea_orm(default_value = false)]
    pub deleted: bool,
    pub deleted_at: Option<DateTime>,
    #[sea_orm(unique)]
    pub event_id: Option<String>,
    pub scheduled_event_id: Option<String>,
    #[sea_orm(default_value = false)]
    pub hide_participants: bool,
    #[sea_orm(default_value = false)]
    pub hide_scores: bool,
    #[sea_orm(default_value = true)]
    pub disable_comments: bool,
    #[sea_orm(default_value = false)]
    pub require_participant_email: bool,
    #[sea_orm(default_value = true)]
    pub allow_tentative_votes: bool,
    #[sea_orm(default_value = false)]
    pub muted: bool,
    pub space_id: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::UserId",
        to = "super::user::Column::Id",
        on_delete = "Cascade"
    )]
    User,
    #[sea_orm(
        belongs_to = "super::scheduled_event::Entity",
        from = "Column::ScheduledEventId",
        to = "super::scheduled_event::Column::Id",
        on_delete = "SetNull"
    )]
    ScheduledEvent,
    #[sea_orm(
        belongs_to = "super::space::Entity",
        from = "Column::SpaceId",
        to = "super::space::Column::Id",
        on_delete = "SetNull"
    )]
    Space,
    #[sea_orm(has_many = "super::option::Entity")]
    Options,
    #[sea_orm(has_many = "super::participant::Entity")]
    Participants,
    #[sea_orm(has_many = "super::comment::Entity")]
    Comments,
    #[sea_orm(has_many = "super::vote::Entity")]
    Votes,
    #[sea_orm(has_many = "super::poll_invite::Entity")]
    Invites,
    #[sea_orm(has_many = "super::poll_activity::Entity")]
    Activities,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

impl Related<super::scheduled_event::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ScheduledEvent.def()
    }
}

impl Related<super::space::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Space.def()
    }
}

impl Related<super::option::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Options.def()
    }
}

impl Related<super::participant::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Participants.def()
    }
}

impl Related<super::comment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Comments.def()
    }
}

impl Related<super::vote::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Votes.def()
    }
}

impl Related<super::poll_invite::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Invites.def()
    }
}

impl Related<super::poll_activity::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Activities.def()
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
