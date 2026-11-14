use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "ApiKey")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: String,
    pub description: Option<String>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "lookupId")]
    pub lookup_id: String,
    #[sea_orm(column_name = "hashedSecret")]
    pub hashed_secret: String,
    #[sea_orm(column_name = "permissionMode", default_value = "all")]
    pub permission_mode: String,
    #[sea_orm(column_name = "userId")]
    pub user_id: Option<String>,
    #[sea_orm(column_name = "createdByUserId")]
    pub created_by_user_id: Option<String>,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    #[sea_orm(column_name = "expiresAt")]
    pub expires_at: Option<DateTime>,
    #[sea_orm(column_name = "lastUsedAt")]
    pub last_used_at: Option<DateTime>,
    #[sea_orm(column_name = "revokedAt")]
    pub revoked_at: Option<DateTime>,
    #[sea_orm(column_name = "revocationCause")]
    pub revocation_cause: Option<String>,
    #[sea_orm(column_name = "parentApiKeyId")]
    pub parent_api_key_id: Option<String>,
    #[sea_orm(column_name = "ingestSourceType")]
    pub ingest_source_type: Option<String>,
    #[sea_orm(column_name = "ingestionTemplateId")]
    pub ingestion_template_id: Option<String>,
    #[sea_orm(column_name = "createdByDeviceLabel")]
    pub created_by_device_label: Option<String>,
    #[sea_orm(column_name = "isSystemManaged", default_value = false)]
    pub is_system_managed: bool,
    #[sea_orm(column_name = "createdByBrowserSessionId")]
    pub created_by_browser_session_id: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::UserId",
        to = "super::user::Column::Id",
        on_delete = "Restrict"
    )]
    User,
    #[sea_orm(
        belongs_to = "super::organization::Entity",
        from = "Column::OrganizationId",
        to = "super::organization::Column::Id",
        on_delete = "Restrict"
    )]
    Organization,
    #[sea_orm(has_many = "super::role_binding::Entity")]
    RoleBindings,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

impl Related<super::organization::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organization.def()
    }
}

impl Related<super::role_binding::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RoleBindings.def()
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
