use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "StoredObject")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "tenantId")]
    pub tenant_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub status: String,
    pub purpose: String,
    #[sea_orm(column_name = "ownerKind")]
    pub owner_kind: String,
    #[sea_orm(column_name = "ownerId")]
    pub owner_id: String,
    pub filename: String,
    pub sha256: String,
    #[sea_orm(column_name = "sizeBytes")]
    pub size_bytes: i64,
    #[sea_orm(column_name = "mediaType")]
    pub media_type: String,
    #[sea_orm(column_name = "mediaTypeVerified", default_value = false)]
    pub media_type_verified: bool,
    #[sea_orm(column_name = "storageProvider")]
    pub storage_provider: Option<String>,
    #[sea_orm(column_name = "storageDestinationId")]
    pub storage_destination_id: Option<String>,
    #[sea_orm(column_name = "storageProviderRelativeId", column_type = "Text")]
    pub storage_provider_relative_id: Option<String>,
    #[sea_orm(default_value = 0)]
    pub generation: i32,
    pub audiences: Json,
    #[sea_orm(column_name = "expiresAt")]
    pub expires_at: Option<DateTime>,
    #[sea_orm(column_name = "availableAt")]
    pub available_at: Option<DateTime>,
    #[sea_orm(column_name = "deletedAt")]
    pub deleted_at: Option<DateTime>,
    pub source: String,
    #[sea_orm(column_name = "legacyFingerprint")]
    pub legacy_fingerprint: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, _insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
