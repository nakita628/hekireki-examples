use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Agent")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    pub name: String,
    pub r#type: String,
    pub config: Json,
    #[sea_orm(column_name = "workflowId")]
    pub workflow_id: Option<String>,
    #[sea_orm(column_name = "copiedFromAgentId")]
    pub copied_from_agent_id: Option<String>,
    pub environment: Option<String>,
    #[sea_orm(column_name = "ownerUserId")]
    pub owner_user_id: Option<String>,
    #[sea_orm(column_name = "hostLabel")]
    pub host_label: Option<String>,
    #[sea_orm(column_name = "identityKey")]
    pub identity_key: Option<String>,
    #[sea_orm(column_name = "lastSeenAt")]
    pub last_seen_at: Option<DateTime>,
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
        belongs_to = "super::agent::Entity",
        from = "Column::CopiedFromAgentId",
        to = "super::agent::Column::Id",
        on_update = "Restrict",
        on_delete = "Restrict"
    )]
    CopiedFrom,
    #[sea_orm(
        has_many = "super::agent::Entity",
        from = "Column::Id",
        to = "super::agent::Column::CopiedFromAgentId"
    )]
    CopiedAgents,
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

impl Related<super::agent::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CopiedFrom.def()
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
