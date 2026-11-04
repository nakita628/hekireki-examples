use super::prisma_date_time::PrismaDateTime;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "document_sync_queues")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    #[sea_orm(column_name = "staleAfterMs", default_value = 604800000)]
    pub stale_after_ms: i32,
    #[sea_orm(column_name = "nextSyncAt")]
    pub next_sync_at: PrismaDateTime,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: PrismaDateTime,
    #[sea_orm(column_name = "lastSyncedAt")]
    pub last_synced_at: PrismaDateTime,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "workspaceDocId")]
    pub workspace_doc_id: i32,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::workspace_documents::Entity",
        from = "Column::WorkspaceDocId",
        to = "super::workspace_documents::Column::Id",
        on_delete = "Cascade"
    )]
    WorkspaceDoc,
    #[sea_orm(has_many = "super::document_sync_executions::Entity")]
    Runs,
}

impl Related<super::workspace_documents::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WorkspaceDoc.def()
    }
}

impl Related<super::document_sync_executions::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Runs.def()
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
        if insert && self.last_synced_at.is_not_set() {
            self.last_synced_at = Set(now.into());
        }
        Ok(self)
    }
}
