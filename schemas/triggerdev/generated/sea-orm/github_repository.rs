use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "GithubRepository")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "githubId")]
    pub github_id: i64,
    pub name: String,
    #[sea_orm(column_name = "fullName")]
    pub full_name: String,
    #[sea_orm(column_name = "htmlUrl")]
    pub html_url: String,
    pub private: bool,
    #[sea_orm(column_name = "defaultBranch")]
    pub default_branch: String,
    #[sea_orm(column_name = "installationId")]
    pub installation_id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::github_app_installation::Entity",
        from = "Column::InstallationId",
        to = "super::github_app_installation::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    Installation,
    #[sea_orm(has_many = "super::connected_github_repository::Entity")]
    ConnectedGithubRepository,
}

impl Related<super::github_app_installation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Installation.def()
    }
}

impl Related<super::connected_github_repository::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ConnectedGithubRepository.def()
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
