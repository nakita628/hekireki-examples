use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "_completedWaitpoints")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false, column_name = "A")]
    pub task_run_execution_snapshot_id: String,
    #[sea_orm(primary_key, auto_increment = false, column_name = "B")]
    pub waitpoint_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::task_run_execution_snapshot::Entity",
        from = "Column::TaskRunExecutionSnapshotId",
        to = "super::task_run_execution_snapshot::Column::Id"
    )]
    TaskRunExecutionSnapshot,
    #[sea_orm(
        belongs_to = "super::waitpoint::Entity",
        from = "Column::WaitpointId",
        to = "super::waitpoint::Column::Id"
    )]
    Waitpoint,
}

impl ActiveModelBehavior for ActiveModel {}
