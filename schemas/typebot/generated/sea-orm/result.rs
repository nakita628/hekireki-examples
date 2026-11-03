use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Result")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "typebotId")]
    pub typebot_id: String,
    pub variables: Json,
    #[sea_orm(column_name = "isCompleted")]
    pub is_completed: bool,
    #[sea_orm(column_name = "hasStarted")]
    pub has_started: Option<bool>,
    #[sea_orm(column_name = "isArchived", default_value = false)]
    pub is_archived: Option<bool>,
    #[sea_orm(column_name = "lastChatSessionId")]
    pub last_chat_session_id: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::typebot::Entity",
        from = "Column::TypebotId",
        to = "super::typebot::Column::Id",
        on_delete = "Cascade"
    )]
    Typebot,
    #[sea_orm(has_many = "super::answer::Entity")]
    Answers,
    #[sea_orm(has_many = "super::answer_v2::Entity")]
    AnswersV2,
    #[sea_orm(has_many = "super::log::Entity")]
    Logs,
    #[sea_orm(has_many = "super::visited_edge::Entity")]
    Edges,
    #[sea_orm(has_many = "super::set_variable_history_item::Entity")]
    SetVariableHistory,
}

impl Related<super::typebot::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Typebot.def()
    }
}

impl Related<super::answer::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Answers.def()
    }
}

impl Related<super::answer_v2::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AnswersV2.def()
    }
}

impl Related<super::log::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Logs.def()
    }
}

impl Related<super::visited_edge::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Edges.def()
    }
}

impl Related<super::set_variable_history_item::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SetVariableHistory.def()
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
        Ok(self)
    }
}
