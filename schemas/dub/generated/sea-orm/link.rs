use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Link")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub domain: String,
    pub key: String,
    #[sea_orm(column_type = "Text")]
    pub url: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "shortLink", column_type = "String(StringLen::N(400))")]
    pub short_link: String,
    #[sea_orm(default_value = false)]
    pub archived: bool,
    #[sea_orm(column_name = "expiresAt")]
    pub expires_at: Option<DateTime>,
    #[sea_orm(column_name = "expiredUrl", column_type = "Text")]
    pub expired_url: Option<String>,
    #[sea_orm(column_name = "disabledAt")]
    pub disabled_at: Option<DateTime>,
    pub password: Option<String>,
    #[sea_orm(column_name = "trackConversion", default_value = false)]
    pub track_conversion: bool,
    #[sea_orm(default_value = false)]
    pub proxy: bool,
    pub title: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(280))")]
    pub description: Option<String>,
    #[sea_orm(column_type = "Text")]
    pub image: Option<String>,
    #[sea_orm(column_type = "Text")]
    pub video: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(255))")]
    pub utm_source: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(255))")]
    pub utm_medium: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(255))")]
    pub utm_campaign: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(255))")]
    pub utm_term: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(255))")]
    pub utm_content: Option<String>,
    #[sea_orm(default_value = false)]
    pub rewrite: bool,
    #[sea_orm(column_name = "linkRetentionCleanupDisabledAt")]
    pub link_retention_cleanup_disabled_at: Option<DateTime>,
    #[sea_orm(column_name = "doIndex", default_value = false)]
    pub do_index: bool,
    #[sea_orm(column_type = "Text")]
    pub ios: Option<String>,
    #[sea_orm(column_type = "Text")]
    pub android: Option<String>,
    pub geo: Option<Json>,
    #[sea_orm(column_name = "testVariants")]
    pub test_variants: Option<Json>,
    #[sea_orm(column_name = "testStartedAt")]
    pub test_started_at: Option<DateTime>,
    #[sea_orm(column_name = "testCompletedAt")]
    pub test_completed_at: Option<DateTime>,
    #[sea_orm(column_name = "userId")]
    pub user_id: Option<String>,
    #[sea_orm(column_name = "projectId")]
    pub project_id: Option<String>,
    #[sea_orm(column_name = "folderId")]
    pub folder_id: Option<String>,
    #[sea_orm(column_name = "externalId")]
    pub external_id: Option<String>,
    #[sea_orm(column_name = "tenantId")]
    pub tenant_id: Option<String>,
    #[sea_orm(column_name = "publicStats", default_value = false)]
    pub public_stats: bool,
    #[sea_orm(default_value = 0)]
    pub clicks: i32,
    #[sea_orm(default_value = 0)]
    pub leads: i32,
    #[sea_orm(default_value = 0)]
    pub conversions: i32,
    #[sea_orm(default_value = 0)]
    pub sales: i32,
    #[sea_orm(column_name = "saleAmount", default_value = "0")]
    pub sale_amount: i64,
    #[sea_orm(column_name = "lastClicked")]
    pub last_clicked: Option<DateTime>,
    #[sea_orm(column_name = "lastLeadAt")]
    pub last_lead_at: Option<DateTime>,
    #[sea_orm(column_name = "lastConversionAt")]
    pub last_conversion_at: Option<DateTime>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_type = "Text")]
    pub comments: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::UserId",
        to = "super::user::Column::Id"
    )]
    User,
    #[sea_orm(
        belongs_to = "super::project::Entity",
        from = "Column::ProjectId",
        to = "super::project::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    Project,
    #[sea_orm(
        belongs_to = "super::folder::Entity",
        from = "Column::FolderId",
        to = "super::folder::Column::Id",
        on_update = "Cascade",
        on_delete = "SetNull"
    )]
    Folder,
    #[sea_orm(
        belongs_to = "super::domain::Entity",
        from = "Column::Domain",
        to = "super::domain::Column::Slug"
    )]
    ShortDomain,
    #[sea_orm(has_many = "super::link_tag::Entity")]
    Tags,
    #[sea_orm(has_many = "super::link_webhook::Entity")]
    Webhooks,
    #[sea_orm(has_many = "super::customer::Entity")]
    Customers,
    #[sea_orm(has_one = "super::dashboard::Entity")]
    Dashboard,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl Related<super::folder::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Folder.def()
    }
}

impl Related<super::domain::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ShortDomain.def()
    }
}

impl Related<super::link_tag::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Tags.def()
    }
}

impl Related<super::link_webhook::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Webhooks.def()
    }
}

impl Related<super::customer::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Customers.def()
    }
}

impl Related<super::dashboard::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Dashboard.def()
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
