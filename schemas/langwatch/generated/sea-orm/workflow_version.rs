use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "WorkflowVersion")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub version: String,
    #[sea_orm(column_name = "commitMessage")]
    pub commit_message: String,
    #[sea_orm(column_name = "authorId")]
    pub author_id: String,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(column_name = "workflowId")]
    pub workflow_id: String,
    #[sea_orm(column_name = "parentId")]
    pub parent_id: Option<String>,
    #[sea_orm(column_name = "autoSaved", default_value = false)]
    pub auto_saved: bool,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    pub dsl: Json,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::AuthorId",
        to = "super::user::Column::Id"
    )]
    Author,
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
        belongs_to = "super::workflow_version::Entity",
        from = "Column::ParentId",
        to = "super::workflow_version::Column::Id",
        on_update = "Restrict",
        on_delete = "Restrict"
    )]
    Parent,
    #[sea_orm(
        has_many = "super::workflow_version::Entity",
        from = "Column::Id",
        to = "super::workflow_version::Column::ParentId"
    )]
    Children,
    #[sea_orm(
        has_many = "super::workflow::Entity",
        from = "Column::Id",
        to = "super::workflow::Column::LatestVersionId"
    )]
    WorkflowAsLatestVersion,
    #[sea_orm(
        has_many = "super::workflow::Entity",
        from = "Column::Id",
        to = "super::workflow::Column::CurrentVersionId"
    )]
    WorkflowAsCurrentVersion,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Author.def()
    }
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

impl Related<super::workflow_version::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Parent.def()
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
