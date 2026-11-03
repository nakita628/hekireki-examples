use super::job_config_state::JobConfigState;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "triggers")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub created_at: DateTime,
    pub updated_at: DateTime,
    pub project_id: String,
    #[sea_orm(column_name = "eventSource")]
    pub event_source: String,
    #[sea_orm(column_name = "eventActions")]
    pub event_actions: Vec<String>,
    pub filter: Option<Json>,
    #[sea_orm(default_value = "ACTIVE")]
    pub status: JobConfigState,
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
    #[sea_orm(has_many = "super::automation_execution::Entity")]
    AutomationExecutions,
    #[sea_orm(has_many = "super::automation::Entity")]
    Automations,
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl Related<super::automation_execution::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AutomationExecutions.def()
    }
}

impl Related<super::automation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Automations.def()
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
