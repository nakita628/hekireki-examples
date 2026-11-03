use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "WorkflowStepLog")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "executionId")]
    pub execution_id: String,
    #[sea_orm(column_name = "workflowStepId")]
    pub workflow_step_id: String,
    #[sea_orm(column_name = "conditionsMatched")]
    pub conditions_matched: bool,
    #[sea_orm(column_name = "conditionResults")]
    pub condition_results: Option<Json>,
    #[sea_orm(column_name = "actionsExecuted")]
    pub actions_executed: Option<Json>,
    #[sea_orm(column_name = "executedAt")]
    pub executed_at: DateTime,
    pub duration: Option<i32>,
    pub error: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::workflow_execution::Entity",
        from = "Column::ExecutionId",
        to = "super::workflow_execution::Column::Id",
        on_delete = "Cascade"
    )]
    Execution,
    #[sea_orm(
        belongs_to = "super::workflow_step::Entity",
        from = "Column::WorkflowStepId",
        to = "super::workflow_step::Column::Id",
        on_delete = "Cascade"
    )]
    Step,
}

impl Related<super::workflow_execution::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Execution.def()
    }
}

impl Related<super::workflow_step::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Step.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.executed_at.is_not_set() {
            self.executed_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
