use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "scimManagedCredential")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "connectionRecordId")]
    pub connection_record_id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "credentialId")]
    pub credential_id: String,
    #[sea_orm(column_name = "tokenDigest")]
    pub token_digest: String,
    #[sea_orm(column_name = "hashVersion")]
    pub hash_version: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "activeSlotKey")]
    pub active_slot_key: String,
    pub status: String,
    #[sea_orm(column_name = "serializedScopes")]
    pub serialized_scopes: String,
    #[sea_orm(column_name = "expiresAt")]
    pub expires_at: DateTime,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "createdBy")]
    pub created_by: String,
    #[sea_orm(column_name = "lastUsedAt")]
    pub last_used_at: Option<DateTime>,
    #[sea_orm(column_name = "revokedAt")]
    pub revoked_at: Option<DateTime>,
    #[sea_orm(column_name = "revokedBy")]
    pub revoked_by: Option<String>,
    #[sea_orm(column_name = "decommissionedAt")]
    pub decommissioned_at: Option<DateTime>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::scim_managed_connection::Entity",
        from = "Column::ConnectionRecordId",
        to = "super::scim_managed_connection::Column::Id",
        on_delete = "Cascade"
    )]
    Scimmanagedconnection,
}

impl Related<super::scim_managed_connection::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Scimmanagedconnection.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
