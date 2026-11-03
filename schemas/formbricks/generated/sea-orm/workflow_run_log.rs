use super::workflow_run_log_status::WorkflowRunLogStatus;
use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "WorkflowRunLog")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "runId")]
    pub run_id: String,
    pub sequence: i32,
    #[sea_orm(column_name = "stepId")]
    pub step_id: String,
    #[sea_orm(column_name = "stepType")]
    pub step_type: String,
    #[sea_orm(default_value = "pending")]
    pub status: WorkflowRunLogStatus,
    #[sea_orm(default_value = "{}")]
    pub input: Json,
    #[sea_orm(default_value = "{}")]
    pub output: Json,
    pub error: Option<String>,
    #[sea_orm(column_name = "startedAt")]
    pub started_at: Option<DateTime>,
    #[sea_orm(column_name = "finishedAt")]
    pub finished_at: Option<DateTime>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::workflow_run::Entity",
        from = "Column::RunId",
        to = "super::workflow_run::Column::Id",
        on_delete = "Cascade"
    )]
    Run,
}

impl Related<super::workflow_run::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Run.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
