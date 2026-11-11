use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_WorkflowRun")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub run_id: String,
    #[sea_orm(unique)]
    pub session_id: String,
    pub workflow_type: String,
    #[sea_orm(default_value = "pending")]
    pub status: String,
    pub created_by: Option<String>,
    pub created_at: DateTime,
    pub updated_at: DateTime,
    pub input: Option<Json>,
    pub output: Option<Json>,
    pub metadata: Option<Json>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::lite_llm_workflow_event::Entity")]
    Events,
    #[sea_orm(has_many = "super::lite_llm_workflow_message::Entity")]
    Messages,
}

impl Related<super::lite_llm_workflow_event::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Events.def()
    }
}

impl Related<super::lite_llm_workflow_message::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Messages.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    fn new() -> Self {
        Self {
            run_id: Set(uuid::Uuid::new_v4().to_string()),
            session_id: Set(uuid::Uuid::new_v4().to_string()),
            ..ActiveModelTrait::default()
        }
    }

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
