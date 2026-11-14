use super::evaluation_execution_mode::EvaluationExecutionMode;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Monitor")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "experimentId")]
    pub experiment_id: Option<String>,
    #[sea_orm(column_name = "evaluatorId")]
    pub evaluator_id: Option<String>,
    #[sea_orm(column_name = "checkType")]
    pub check_type: String,
    pub name: String,
    pub slug: String,
    #[sea_orm(column_name = "executionMode", default_value = "ON_MESSAGE")]
    pub execution_mode: EvaluationExecutionMode,
    #[sea_orm(default_value = true)]
    pub enabled: bool,
    pub preconditions: Json,
    pub parameters: Json,
    pub mappings: Option<Json>,
    #[sea_orm(default_value = 1)]
    pub sample: f64,
    #[sea_orm(default_value = "trace")]
    pub level: String,
    #[sea_orm(column_name = "threadIdleTimeout")]
    pub thread_idle_timeout: Option<i32>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "autoPausedAt")]
    pub auto_paused_at: Option<DateTime>,
    #[sea_orm(column_name = "autoPausedReason")]
    pub auto_paused_reason: Option<String>,
    #[sea_orm(column_name = "evaluatorLabel")]
    pub evaluator_label: Option<String>,
    #[sea_orm(column_name = "createdById")]
    pub created_by_id: Option<String>,
    #[sea_orm(column_name = "updatedById")]
    pub updated_by_id: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::project::Entity",
        from = "Column::ProjectId",
        to = "super::project::Column::Id"
    )]
    Project,
    #[sea_orm(
        belongs_to = "super::experiment::Entity",
        from = "Column::ExperimentId",
        to = "super::experiment::Column::Id",
        on_update = "Restrict",
        on_delete = "Restrict"
    )]
    Experiment,
    #[sea_orm(
        belongs_to = "super::evaluator::Entity",
        from = "Column::EvaluatorId",
        to = "super::evaluator::Column::Id"
    )]
    Evaluator,
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl Related<super::experiment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Experiment.def()
    }
}

impl Related<super::evaluator::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Evaluator.def()
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
