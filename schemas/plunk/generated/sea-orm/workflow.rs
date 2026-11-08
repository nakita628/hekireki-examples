use super::workflow_trigger_type::WorkflowTriggerType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "workflows")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: String,
    pub description: Option<String>,
    #[sea_orm(default_value = false)]
    pub enabled: bool,
    #[sea_orm(column_name = "triggerType")]
    pub trigger_type: WorkflowTriggerType,
    #[sea_orm(column_name = "triggerConfig")]
    pub trigger_config: Option<Json>,
    #[sea_orm(column_name = "allowReentry", default_value = false)]
    pub allow_reentry: bool,
    #[sea_orm(column_name = "throttleWindowSeconds")]
    pub throttle_window_seconds: Option<i32>,
    #[sea_orm(column_name = "throttleKey")]
    pub throttle_key: Option<String>,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
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
    #[sea_orm(has_many = "super::workflow_step::Entity")]
    Steps,
    #[sea_orm(has_many = "super::workflow_execution::Entity")]
    Executions,
    #[sea_orm(has_many = "super::throttle_claim::Entity")]
    ThrottleClaims,
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl Related<super::workflow_step::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Steps.def()
    }
}

impl Related<super::workflow_execution::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Executions.def()
    }
}

impl Related<super::throttle_claim::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ThrottleClaims.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    fn new() -> Self {
        Self {
            id: Set(uuid::Uuid::new_v4().to_string()),
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
