use super::github_repository_selection::GithubRepositorySelection;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "GithubAppInstallation")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "appInstallationId")]
    pub app_installation_id: i64,
    #[sea_orm(column_name = "targetId")]
    pub target_id: i64,
    #[sea_orm(column_name = "targetType")]
    pub target_type: String,
    #[sea_orm(column_name = "accountHandle")]
    pub account_handle: String,
    pub permissions: Option<Json>,
    #[sea_orm(column_name = "repositorySelection")]
    pub repository_selection: GithubRepositorySelection,
    #[sea_orm(column_name = "installedBy")]
    pub installed_by: Option<String>,
    #[sea_orm(column_name = "installedByUserId")]
    pub installed_by_user_id: Option<String>,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    #[sea_orm(column_name = "deletedAt")]
    pub deleted_at: Option<DateTime>,
    #[sea_orm(column_name = "suspendedAt")]
    pub suspended_at: Option<DateTime>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::organization::Entity",
        from = "Column::OrganizationId",
        to = "super::organization::Column::Id"
    )]
    Organization,
    #[sea_orm(has_many = "super::github_repository::Entity")]
    Repositories,
}

impl Related<super::organization::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organization.def()
    }
}

impl Related<super::github_repository::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Repositories.def()
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
