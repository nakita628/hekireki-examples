use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "SocialMediaAgency")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "userId")]
    pub user_id: String,
    pub name: String,
    #[sea_orm(column_name = "logoId")]
    pub logo_id: Option<String>,
    pub website: Option<String>,
    pub slug: Option<String>,
    pub facebook: Option<String>,
    pub instagram: Option<String>,
    pub twitter: Option<String>,
    #[sea_orm(column_name = "linkedIn")]
    pub linked_in: Option<String>,
    pub youtube: Option<String>,
    pub tiktok: Option<String>,
    #[sea_orm(column_name = "otherSocialMedia")]
    pub other_social_media: Option<String>,
    #[sea_orm(column_name = "shortDescription")]
    pub short_description: String,
    pub description: String,
    #[sea_orm(default_value = false)]
    pub approved: bool,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "deletedAt")]
    pub deleted_at: Option<DateTime>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::media::Entity",
        from = "Column::LogoId",
        to = "super::media::Column::Id"
    )]
    Logo,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::UserId",
        to = "super::user::Column::Id"
    )]
    User,
    #[sea_orm(has_many = "super::social_media_agency_niche::Entity")]
    Niches,
}

impl Related<super::media::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Logo.def()
    }
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

impl Related<super::social_media_agency_niche::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Niches.def()
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
