use super::document_storage_type::DocumentStorageType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "DocumentPage")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "versionId")]
    pub version_id: String,
    #[sea_orm(column_name = "pageNumber")]
    pub page_number: i32,
    #[sea_orm(column_name = "embeddedLinks")]
    pub embedded_links: Vec<String>,
    #[sea_orm(column_name = "pageLinks")]
    pub page_links: Option<Json>,
    pub metadata: Option<Json>,
    pub file: String,
    #[sea_orm(column_name = "storageType", default_value = "VERCEL_BLOB")]
    pub storage_type: DocumentStorageType,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::document_version::Entity",
        from = "Column::VersionId",
        to = "super::document_version::Column::Id",
        on_delete = "Cascade"
    )]
    Version,
}

impl Related<super::document_version::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Version.def()
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
