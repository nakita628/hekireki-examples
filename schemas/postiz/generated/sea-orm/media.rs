use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Media")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: String,
    #[sea_orm(column_name = "originalName")]
    pub original_name: Option<String>,
    pub path: String,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "deletedAt")]
    pub deleted_at: Option<DateTime>,
    #[sea_orm(column_name = "fileSize", default_value = 0)]
    pub file_size: i32,
    #[sea_orm(default_value = "image")]
    pub r#type: String,
    pub thumbnail: Option<String>,
    pub alt: Option<String>,
    #[sea_orm(column_name = "thumbnailTimestamp")]
    pub thumbnail_timestamp: Option<i32>,
    #[sea_orm(default_value = "ready")]
    pub status: String,
    #[sea_orm(column_name = "processingError")]
    pub processing_error: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::organization::Entity",
        from = "Column::OrganizationId",
        to = "super::organization::Column::Id"
    )]
    Organization,
    #[sea_orm(has_many = "super::social_media_agency::Entity")]
    Agencies,
    #[sea_orm(has_many = "super::user::Entity")]
    UserPicture,
    #[sea_orm(has_many = "super::oauth_app::Entity")]
    OauthApps,
}

impl Related<super::organization::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organization.def()
    }
}

impl Related<super::social_media_agency::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Agencies.def()
    }
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::UserPicture.def()
    }
}

impl Related<super::oauth_app::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OauthApps.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    fn new() -> Self {
        Self {
            id: Set(uuid::Uuid::new_v4().to_string()),
            ..ActiveModelTrait::default()
        }
    }

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
