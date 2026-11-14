use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "DatasetVersion")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(column_name = "datasetId")]
    pub dataset_id: String,
    pub version: i32,
    pub name: Option<String>,
    pub trigger: String,
    #[sea_orm(column_name = "contentHash")]
    pub content_hash: String,
    #[sea_orm(column_name = "columnTypes")]
    pub column_types: Json,
    #[sea_orm(column_name = "rowCount")]
    pub row_count: i32,
    #[sea_orm(column_name = "sizeBytes")]
    pub size_bytes: i64,
    #[sea_orm(column_name = "chunkCount")]
    pub chunk_count: i32,
    #[sea_orm(column_name = "restoredFromVersion")]
    pub restored_from_version: Option<i32>,
    #[sea_orm(column_name = "authorId")]
    pub author_id: Option<String>,
    #[sea_orm(column_name = "authorKind")]
    pub author_kind: Option<String>,
    #[sea_orm(column_name = "pinnedAt")]
    pub pinned_at: Option<DateTime>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

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
        Ok(self)
    }
}
