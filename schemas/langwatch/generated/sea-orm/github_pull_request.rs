use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "GithubPullRequest")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    #[sea_orm(column_name = "repositoryHost")]
    pub repository_host: String,
    #[sea_orm(column_name = "repositoryFullName")]
    pub repository_full_name: String,
    #[sea_orm(column_name = "headBranch")]
    pub head_branch: String,
    #[sea_orm(column_name = "prNumber")]
    pub pr_number: i32,
    #[sea_orm(column_name = "htmlUrl")]
    pub html_url: String,
    pub title: String,
    pub state: String,
    #[sea_orm(column_name = "isDraft", default_value = false)]
    pub is_draft: bool,
    #[sea_orm(column_name = "authorLogin")]
    pub author_login: Option<String>,
    #[sea_orm(column_name = "prCreatedAt")]
    pub pr_created_at: DateTime,
    #[sea_orm(column_name = "prClosedAt")]
    pub pr_closed_at: Option<DateTime>,
    #[sea_orm(column_name = "prMergedAt")]
    pub pr_merged_at: Option<DateTime>,
    #[sea_orm(column_name = "prUpdatedAt")]
    pub pr_updated_at: Option<DateTime>,
    #[sea_orm(column_name = "mappedAt")]
    pub mapped_at: DateTime,
    #[sea_orm(column_name = "lastCheckedAt")]
    pub last_checked_at: DateTime,
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
        to = "super::organization::Column::Id",
        on_delete = "Cascade"
    )]
    Organization,
}

impl Related<super::organization::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organization.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.mapped_at.is_not_set() {
            self.mapped_at = Set(now.naive_utc());
        }
        if insert && self.last_checked_at.is_not_set() {
            self.last_checked_at = Set(now.naive_utc());
        }
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.naive_utc());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
