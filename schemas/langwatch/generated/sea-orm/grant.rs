use super::grant_principal_type::GrantPrincipalType;
use super::grant_scope_type::GrantScopeType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Grant")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    #[sea_orm(column_name = "principalType")]
    pub principal_type: GrantPrincipalType,
    #[sea_orm(column_name = "principalId")]
    pub principal_id: Option<String>,
    #[sea_orm(column_name = "roleKey")]
    pub role_key: Option<String>,
    #[sea_orm(column_name = "legacyRole")]
    pub legacy_role: Option<String>,
    pub source: String,
    #[sea_orm(column_name = "scopeType")]
    pub scope_type: GrantScopeType,
    #[sea_orm(column_name = "scopeId")]
    pub scope_id: String,
    #[sea_orm(unique)]
    pub token: Option<String>,
    pub permission: Option<String>,
    #[sea_orm(column_name = "resourceKind")]
    pub resource_kind: Option<String>,
    #[sea_orm(column_name = "projectId")]
    pub project_id: Option<String>,
    #[sea_orm(column_name = "createdByUserId")]
    pub created_by_user_id: Option<String>,
    #[sea_orm(column_name = "expiresAt")]
    pub expires_at: Option<DateTime>,
    #[sea_orm(column_name = "maxViews")]
    pub max_views: Option<i32>,
    pub condition: Option<Json>,
    #[sea_orm(column_name = "replacesGrantId")]
    pub replaces_grant_id: Option<String>,
    #[sea_orm(column_name = "occurredAt")]
    pub occurred_at: DateTime,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "revokedAt")]
    pub revoked_at: Option<DateTime>,
    #[sea_orm(column_name = "revokedReason")]
    pub revoked_reason: Option<String>,
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
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
