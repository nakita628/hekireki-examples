use super::api_key_scope::ApiKeyScope;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "api_keys")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub created_at: DateTime,
    pub note: Option<String>,
    #[sea_orm(unique)]
    pub public_key: String,
    #[sea_orm(unique)]
    pub hashed_secret_key: String,
    #[sea_orm(unique)]
    pub fast_hashed_secret_key: Option<String>,
    pub display_secret_key: String,
    pub last_used_at: Option<DateTime>,
    pub expires_at: Option<DateTime>,
    #[sea_orm(default_value = false)]
    pub is_in_app_agent_key: bool,
    pub project_id: Option<String>,
    #[sea_orm(column_name = "organization_id")]
    pub org_id: Option<String>,
    #[sea_orm(default_value = "PROJECT")]
    pub scope: ApiKeyScope,
    pub created_by_user_id: Option<String>,
    pub user_id: Option<String>,
    pub created_by_api_key_id: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::project::Entity",
        from = "Column::ProjectId",
        to = "super::project::Column::Id",
        on_delete = "Cascade"
    )]
    Project,
    #[sea_orm(
        belongs_to = "super::organization::Entity",
        from = "Column::OrgId",
        to = "super::organization::Column::Id",
        on_delete = "Cascade"
    )]
    Organization,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::CreatedByUserId",
        to = "super::user::Column::Id",
        on_delete = "SetNull"
    )]
    CreatedByUser,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::UserId",
        to = "super::user::Column::Id",
        on_delete = "Cascade"
    )]
    User,
    #[sea_orm(
        belongs_to = "super::api_key::Entity",
        from = "Column::CreatedByApiKeyId",
        to = "super::api_key::Column::Id",
        on_delete = "SetNull"
    )]
    CreatedByApiKey,
    #[sea_orm(
        has_many = "super::api_key::Entity",
        from = "Column::Id",
        to = "super::api_key::Column::CreatedByApiKeyId"
    )]
    ApiKeysCreated,
    #[sea_orm(has_many = "super::role_assignment::Entity")]
    RoleAssignments,
    #[sea_orm(has_one = "super::gateway_api_key_association::Entity")]
    GatewayAssociation,
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl Related<super::organization::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organization.def()
    }
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CreatedByUser.def()
    }
}

impl Related<super::api_key::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CreatedByApiKey.def()
    }
}

impl Related<super::role_assignment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RoleAssignments.def()
    }
}

impl Related<super::gateway_api_key_association::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::GatewayAssociation.def()
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
        Ok(self)
    }
}
