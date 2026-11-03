use super::provider::Provider;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "User")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub email: String,
    pub password: Option<String>,
    #[sea_orm(column_name = "providerName")]
    pub provider_name: Provider,
    pub name: Option<String>,
    #[sea_orm(column_name = "lastName")]
    pub last_name: Option<String>,
    #[sea_orm(column_name = "isSuperAdmin", default_value = false)]
    pub is_super_admin: bool,
    pub bio: Option<String>,
    #[sea_orm(default_value = 0)]
    pub audience: i32,
    #[sea_orm(column_name = "pictureId")]
    pub picture_id: Option<String>,
    #[sea_orm(column_name = "providerId")]
    pub provider_id: Option<String>,
    pub timezone: i32,
    #[sea_orm(column_name = "timezoneName")]
    pub timezone_name: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "lastReadNotifications")]
    pub last_read_notifications: DateTime,
    #[sea_orm(column_name = "inviteId")]
    pub invite_id: Option<String>,
    #[sea_orm(default_value = true)]
    pub activated: bool,
    pub account: Option<String>,
    #[sea_orm(column_name = "connectedAccount", default_value = false)]
    pub connected_account: bool,
    #[sea_orm(column_name = "lastOnline")]
    pub last_online: DateTime,
    pub ip: Option<String>,
    pub agent: Option<String>,
    #[sea_orm(column_name = "deletedAt")]
    pub deleted_at: Option<DateTime>,
    #[sea_orm(column_name = "sendSuccessEmails", default_value = true)]
    pub send_success_emails: bool,
    #[sea_orm(column_name = "sendFailureEmails", default_value = true)]
    pub send_failure_emails: bool,
    #[sea_orm(column_name = "sendStreakEmails", default_value = true)]
    pub send_streak_emails: bool,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::media::Entity",
        from = "Column::PictureId",
        to = "super::media::Column::Id"
    )]
    Picture,
    #[sea_orm(has_many = "super::comments::Entity")]
    Comments,
    #[sea_orm(has_many = "super::item_user::Entity")]
    Items,
    #[sea_orm(
        has_many = "super::messages_group::Entity",
        from = "Column::Id",
        to = "super::messages_group::Column::BuyerId"
    )]
    GroupBuyer,
    #[sea_orm(
        has_many = "super::messages_group::Entity",
        from = "Column::Id",
        to = "super::messages_group::Column::SellerId"
    )]
    GroupSeller,
    #[sea_orm(
        has_many = "super::orders::Entity",
        from = "Column::Id",
        to = "super::orders::Column::BuyerId"
    )]
    OrderBuyer,
    #[sea_orm(
        has_many = "super::orders::Entity",
        from = "Column::Id",
        to = "super::orders::Column::SellerId"
    )]
    OrderSeller,
    #[sea_orm(has_many = "super::payout_problems::Entity")]
    PayoutProblems,
    #[sea_orm(has_many = "super::user_organization::Entity")]
    Organizations,
    #[sea_orm(has_many = "super::oauth_authorization::Entity")]
    OauthAuthorizations,
    #[sea_orm(has_one = "super::social_media_agency::Entity")]
    Agencies,
}

impl Related<super::media::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Picture.def()
    }
}

impl Related<super::comments::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Comments.def()
    }
}

impl Related<super::item_user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Items.def()
    }
}

impl Related<super::messages_group::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::GroupBuyer.def()
    }
}

impl Related<super::orders::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OrderBuyer.def()
    }
}

impl Related<super::payout_problems::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PayoutProblems.def()
    }
}

impl Related<super::user_organization::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organizations.def()
    }
}

impl Related<super::oauth_authorization::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OauthAuthorizations.def()
    }
}

impl Related<super::social_media_agency::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Agencies.def()
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
        if insert && self.last_read_notifications.is_not_set() {
            self.last_read_notifications = Set(now.naive_utc());
        }
        if insert && self.last_online.is_not_set() {
            self.last_online = Set(now.naive_utc());
        }
        Ok(self)
    }
}
