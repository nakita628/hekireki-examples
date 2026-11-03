use super::document_storage_type::DocumentStorageType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Document")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: String,
    pub description: Option<String>,
    pub file: String,
    #[sea_orm(column_name = "originalFile")]
    pub original_file: Option<String>,
    pub r#type: Option<String>,
    #[sea_orm(column_name = "contentType")]
    pub content_type: Option<String>,
    #[sea_orm(column_name = "storageType", default_value = "VERCEL_BLOB")]
    pub storage_type: DocumentStorageType,
    #[sea_orm(column_name = "numPages")]
    pub num_pages: Option<i32>,
    #[sea_orm(column_name = "teamId")]
    pub team_id: String,
    #[sea_orm(column_name = "ownerId")]
    pub owner_id: Option<String>,
    #[sea_orm(column_name = "assistantEnabled", default_value = false)]
    pub assistant_enabled: bool,
    #[sea_orm(column_name = "advancedExcelEnabled", default_value = false)]
    pub advanced_excel_enabled: bool,
    #[sea_orm(column_name = "agentsEnabled", default_value = false)]
    pub agents_enabled: bool,
    #[sea_orm(column_name = "downloadOnly", default_value = false)]
    pub download_only: bool,
    #[sea_orm(column_name = "hiddenInAllDocuments", default_value = false)]
    pub hidden_in_all_documents: bool,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "folderId")]
    pub folder_id: Option<String>,
    #[sea_orm(column_name = "isExternalUpload", default_value = false)]
    pub is_external_upload: bool,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::OwnerId",
        to = "super::user::Column::Id",
        on_delete = "SetNull"
    )]
    Owner,
    #[sea_orm(
        belongs_to = "super::team::Entity",
        from = "Column::TeamId",
        to = "super::team::Column::Id",
        on_delete = "Cascade"
    )]
    Team,
    #[sea_orm(
        belongs_to = "super::folder::Entity",
        from = "Column::FolderId",
        to = "super::folder::Column::Id",
        on_delete = "SetNull"
    )]
    Folder,
    #[sea_orm(has_many = "super::link::Entity")]
    Links,
    #[sea_orm(has_many = "super::view::Entity")]
    Views,
    #[sea_orm(has_many = "super::document_version::Entity")]
    Versions,
    #[sea_orm(has_many = "super::chat::Entity")]
    Chats,
    #[sea_orm(has_many = "super::dataroom_document::Entity")]
    Datarooms,
    #[sea_orm(has_many = "super::tag_item::Entity")]
    Tags,
    #[sea_orm(has_many = "super::document_annotation::Entity")]
    Annotations,
    #[sea_orm(has_many = "super::document_redaction_job::Entity")]
    RedactionJobs,
    #[sea_orm(has_many = "super::document_upload::Entity")]
    UploadedDocument,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Owner.def()
    }
}

impl Related<super::team::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Team.def()
    }
}

impl Related<super::folder::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Folder.def()
    }
}

impl Related<super::link::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Links.def()
    }
}

impl Related<super::view::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Views.def()
    }
}

impl Related<super::document_version::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Versions.def()
    }
}

impl Related<super::chat::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Chats.def()
    }
}

impl Related<super::dataroom_document::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Datarooms.def()
    }
}

impl Related<super::tag_item::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Tags.def()
    }
}

impl Related<super::document_annotation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Annotations.def()
    }
}

impl Related<super::document_redaction_job::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RedactionJobs.def()
    }
}

impl Related<super::document_upload::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::UploadedDocument.def()
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
