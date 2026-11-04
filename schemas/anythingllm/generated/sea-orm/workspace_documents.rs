use super::prisma_date_time::PrismaDateTime;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "workspace_documents")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "docId")]
    pub doc_id: String,
    pub filename: String,
    pub docpath: String,
    #[sea_orm(column_name = "workspaceId")]
    pub workspace_id: i32,
    pub metadata: Option<String>,
    #[sea_orm(default_value = false)]
    pub pinned: Option<bool>,
    #[sea_orm(default_value = false)]
    pub watched: Option<bool>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: PrismaDateTime,
    #[sea_orm(column_name = "lastUpdatedAt")]
    pub last_updated_at: PrismaDateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::workspaces::Entity",
        from = "Column::WorkspaceId",
        to = "super::workspaces::Column::Id"
    )]
    Workspace,
    #[sea_orm(has_one = "super::document_sync_queues::Entity")]
    DocumentSyncQueues,
}

impl Related<super::workspaces::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Workspace.def()
    }
}

impl Related<super::document_sync_queues::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DocumentSyncQueues.def()
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
            self.created_at = Set(now.into());
        }
        if insert && self.last_updated_at.is_not_set() {
            self.last_updated_at = Set(now.into());
        }
        Ok(self)
    }
}
