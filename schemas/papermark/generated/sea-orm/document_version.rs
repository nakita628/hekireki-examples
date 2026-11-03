use super::document_storage_type::DocumentStorageType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "DocumentVersion")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "versionNumber")]
    pub version_number: i32,
    #[sea_orm(column_name = "documentId")]
    pub document_id: String,
    pub file: String,
    #[sea_orm(column_name = "originalFile")]
    pub original_file: Option<String>,
    pub r#type: Option<String>,
    #[sea_orm(column_name = "contentType")]
    pub content_type: Option<String>,
    #[sea_orm(column_name = "fileSize")]
    pub file_size: Option<i64>,
    #[sea_orm(column_name = "storageType", default_value = "VERCEL_BLOB")]
    pub storage_type: DocumentStorageType,
    #[sea_orm(column_name = "numPages")]
    pub num_pages: Option<i32>,
    #[sea_orm(column_name = "isPrimary", default_value = false)]
    pub is_primary: bool,
    #[sea_orm(column_name = "isVertical", default_value = false)]
    pub is_vertical: bool,
    #[sea_orm(column_name = "fileId")]
    pub file_id: Option<String>,
    #[sea_orm(column_name = "vectorStoreFileId")]
    pub vector_store_file_id: Option<String>,
    #[sea_orm(column_name = "hasPages", default_value = false)]
    pub has_pages: bool,
    pub length: Option<i32>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::document::Entity",
        from = "Column::DocumentId",
        to = "super::document::Column::Id",
        on_delete = "Cascade"
    )]
    Document,
    #[sea_orm(has_many = "super::document_page::Entity")]
    Pages,
    #[sea_orm(has_many = "super::document_redaction_job::Entity")]
    RedactionJobs,
}

impl Related<super::document::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Document.def()
    }
}

impl Related<super::document_page::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Pages.def()
    }
}

impl Related<super::document_redaction_job::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RedactionJobs.def()
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
