use super::action_execution_status::ActionExecutionStatus;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "automation_executions")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub created_at: DateTime,
    pub updated_at: DateTime,
    pub source_id: String,
    pub automation_id: String,
    pub trigger_id: String,
    pub action_id: String,
    pub project_id: String,
    #[sea_orm(default_value = "PENDING")]
    pub status: ActionExecutionStatus,
    pub input: Json,
    pub output: Option<Json>,
    pub started_at: Option<DateTime>,
    pub finished_at: Option<DateTime>,
    pub error: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::automation::Entity",
        from = "Column::AutomationId",
        to = "super::automation::Column::Id",
        on_delete = "Cascade"
    )]
    Automation,
    #[sea_orm(
        belongs_to = "super::trigger::Entity",
        from = "Column::TriggerId",
        to = "super::trigger::Column::Id",
        on_delete = "Cascade"
    )]
    Trigger,
    #[sea_orm(
        belongs_to = "super::action::Entity",
        from = "Column::ActionId",
        to = "super::action::Column::Id",
        on_delete = "Cascade"
    )]
    Action,
    #[sea_orm(
        belongs_to = "super::project::Entity",
        from = "Column::ProjectId",
        to = "super::project::Column::Id",
        on_delete = "Cascade"
    )]
    Project,
}

impl Related<super::automation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Automation.def()
    }
}

impl Related<super::trigger::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Trigger.def()
    }
}

impl Related<super::action::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Action.def()
    }
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
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
