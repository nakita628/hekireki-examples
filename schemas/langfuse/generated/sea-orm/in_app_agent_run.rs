use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "in_app_agent_runs")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub project_id: String,
    pub conversation_id: String,
    pub triggered_by_user_id: Option<String>,
    pub model: Option<String>,
    pub mcp_api_key_id: Option<String>,
    pub error_code: Option<String>,
    pub error_message: Option<String>,
    pub finished_at: Option<DateTime>,
    pub status: Option<String>,
    pub request: Option<Json>,
    pub claimed_at: Option<DateTime>,
    pub heartbeat_at: Option<DateTime>,
    pub cancel_requested_at: Option<DateTime>,
    pub created_at: DateTime,
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::project::Entity",
        from = "Column::ProjectId",
        to = "super::project::Column::Id",
        on_delete = "Cascade"
    )]
    Project,
    #[sea_orm(
        belongs_to = "super::in_app_agent_conversation::Entity",
        from = "(Column::ConversationId, Column::ProjectId)",
        to = "(super::in_app_agent_conversation::Column::Id, super::in_app_agent_conversation::Column::ProjectId)",
        on_delete = "Cascade"
    )]
    Conversation,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::TriggeredByUserId",
        to = "super::user::Column::Id",
        on_delete = "SetNull"
    )]
    TriggeredByUser,
    #[sea_orm(has_many = "super::in_app_agent_event::Entity")]
    Events,
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl Related<super::in_app_agent_conversation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Conversation.def()
    }
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TriggeredByUser.def()
    }
}

impl Related<super::in_app_agent_event::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Events.def()
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
