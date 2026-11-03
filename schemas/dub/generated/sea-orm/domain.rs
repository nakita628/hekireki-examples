use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Domain")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    pub slug: String,
    #[sea_orm(default_value = false)]
    pub verified: bool,
    pub placeholder: Option<String>,
    #[sea_orm(column_name = "expiredUrl", column_type = "Text")]
    pub expired_url: Option<String>,
    #[sea_orm(column_name = "notFoundUrl", column_type = "Text")]
    pub not_found_url: Option<String>,
    #[sea_orm(default_value = false)]
    pub primary: bool,
    #[sea_orm(default_value = false)]
    pub archived: bool,
    #[sea_orm(column_name = "lastChecked")]
    pub last_checked: DateTime,
    pub logo: Option<String>,
    #[sea_orm(column_name = "appleAppSiteAssociation")]
    pub apple_app_site_association: Option<Json>,
    #[sea_orm(column_name = "assetLinks")]
    pub asset_links: Option<Json>,
    #[sea_orm(column_name = "deepviewData", default_value = "{}")]
    pub deepview_data: Option<Json>,
    #[sea_orm(column_name = "linkRetentionDays")]
    pub link_retention_days: Option<i32>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "projectId")]
    pub project_id: Option<String>,
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
    #[sea_orm(has_many = "super::link::Entity")]
    Links,
    #[sea_orm(has_one = "super::registered_domain::Entity")]
    RegisteredDomain,
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl Related<super::link::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Links.def()
    }
}

impl Related<super::registered_domain::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RegisteredDomain.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.last_checked.is_not_set() {
            self.last_checked = Set(now.naive_utc());
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
