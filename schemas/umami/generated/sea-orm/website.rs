use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "website")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "website_id", column_type = "Uuid")]
    pub id: String,
    #[sea_orm(column_type = "String(StringLen::N(100))")]
    pub name: String,
    #[sea_orm(column_type = "String(StringLen::N(500))")]
    pub domain: Option<String>,
    #[sea_orm(column_type = "TimestampWithTimeZone")]
    pub reset_at: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_type = "Uuid")]
    pub user_id: Option<String>,
    #[sea_orm(column_type = "Uuid")]
    pub team_id: Option<String>,
    #[sea_orm(column_type = "Uuid")]
    pub created_by: Option<String>,
    #[sea_orm(column_type = "TimestampWithTimeZone")]
    pub created_at: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_type = "TimestampWithTimeZone")]
    pub updated_at: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_type = "TimestampWithTimeZone")]
    pub deleted_at: Option<DateTimeWithTimeZone>,
    #[sea_orm(default_value = false)]
    pub recorder_enabled: bool,
    pub replay_config: Option<Json>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::UserId",
        to = "super::user::Column::Id"
    )]
    User,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::CreatedBy",
        to = "super::user::Column::Id"
    )]
    CreateUser,
    #[sea_orm(
        belongs_to = "super::team::Entity",
        from = "Column::TeamId",
        to = "super::team::Column::Id"
    )]
    Team,
    #[sea_orm(has_many = "super::annotation::Entity")]
    Annotations,
    #[sea_orm(has_many = "super::event_data::Entity")]
    EventData,
    #[sea_orm(has_many = "super::report::Entity")]
    Reports,
    #[sea_orm(has_many = "super::revenue::Entity")]
    Revenue,
    #[sea_orm(has_many = "super::segment::Entity")]
    Segments,
    #[sea_orm(has_many = "super::session_data::Entity")]
    SessionData,
    #[sea_orm(has_many = "super::session_replay::Entity")]
    SessionReplays,
    #[sea_orm(has_many = "super::session_replay_saved::Entity")]
    SessionReplaysSaved,
    #[sea_orm(has_many = "super::heatmap_event::Entity")]
    HeatmapEvents,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

impl Related<super::team::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Team.def()
    }
}

impl Related<super::annotation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Annotations.def()
    }
}

impl Related<super::event_data::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EventData.def()
    }
}

impl Related<super::report::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Reports.def()
    }
}

impl Related<super::revenue::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Revenue.def()
    }
}

impl Related<super::segment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Segments.def()
    }
}

impl Related<super::session_data::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SessionData.def()
    }
}

impl Related<super::session_replay::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SessionReplays.def()
    }
}

impl Related<super::session_replay_saved::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SessionReplaysSaved.def()
    }
}

impl Related<super::heatmap_event::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::HeatmapEvents.def()
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
            self.created_at = Set(Some(now.fixed_offset()));
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(Some(now.fixed_offset()));
        }
        Ok(self)
    }
}
