use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Dataset")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    pub name: String,
    pub slug: String,
    #[sea_orm(column_name = "columnTypes")]
    pub column_types: Json,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "archivedAt")]
    pub archived_at: Option<DateTime>,
    pub mapping: Option<Json>,
    #[sea_orm(column_name = "useS3", default_value = false)]
    pub use_s3: bool,
    #[sea_orm(column_name = "s3RecordCount")]
    pub s3_record_count: Option<i32>,
    #[sea_orm(column_name = "contentLayout", default_value = "postgres")]
    pub content_layout: String,
    #[sea_orm(default_value = "ready")]
    pub status: String,
    #[sea_orm(column_name = "statusError")]
    pub status_error: Option<String>,
    #[sea_orm(column_name = "stagingKey")]
    pub staging_key: Option<String>,
    #[sea_orm(column_name = "sourceStoredObjectId")]
    pub source_stored_object_id: Option<String>,
    #[sea_orm(column_name = "uploadFilename")]
    pub upload_filename: Option<String>,
    #[sea_orm(column_name = "rowCount")]
    pub row_count: Option<i32>,
    #[sea_orm(column_name = "sizeBytes")]
    pub size_bytes: Option<i64>,
    #[sea_orm(column_name = "chunkCount")]
    pub chunk_count: Option<i32>,
    #[sea_orm(column_name = "chunkOffsets")]
    pub chunk_offsets: Option<Json>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::project::Entity",
        from = "Column::ProjectId",
        to = "super::project::Column::Id"
    )]
    Project,
    #[sea_orm(has_many = "super::dataset_record::Entity")]
    DatasetRecords,
    #[sea_orm(has_many = "super::batch_evaluation::Entity")]
    BatchEvaluations,
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl Related<super::dataset_record::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DatasetRecords.def()
    }
}

impl Related<super::batch_evaluation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BatchEvaluations.def()
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
