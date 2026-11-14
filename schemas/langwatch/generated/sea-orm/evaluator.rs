use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Evaluator")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    pub name: String,
    pub slug: Option<String>,
    pub r#type: String,
    pub config: Json,
    #[sea_orm(column_name = "workflowId")]
    pub workflow_id: Option<String>,
    #[sea_orm(column_name = "copiedFromEvaluatorId")]
    pub copied_from_evaluator_id: Option<String>,
    #[sea_orm(column_name = "archivedAt")]
    pub archived_at: Option<DateTime>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
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
        belongs_to = "super::workflow::Entity",
        from = "Column::WorkflowId",
        to = "super::workflow::Column::Id"
    )]
    Workflow,
    #[sea_orm(
        belongs_to = "super::evaluator::Entity",
        from = "Column::CopiedFromEvaluatorId",
        to = "super::evaluator::Column::Id",
        on_update = "Restrict",
        on_delete = "Restrict"
    )]
    CopiedFrom,
    #[sea_orm(
        has_many = "super::evaluator::Entity",
        from = "Column::Id",
        to = "super::evaluator::Column::CopiedFromEvaluatorId"
    )]
    CopiedEvaluators,
    #[sea_orm(has_many = "super::monitor::Entity")]
    Monitors,
    #[sea_orm(has_many = "super::gateway_guardrail::Entity")]
    GatewayGuardrails,
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl Related<super::workflow::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Workflow.def()
    }
}

impl Related<super::evaluator::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CopiedFrom.def()
    }
}

impl Related<super::monitor::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Monitors.def()
    }
}

impl Related<super::gateway_guardrail::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::GatewayGuardrails.def()
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
