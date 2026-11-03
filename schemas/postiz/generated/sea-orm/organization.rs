use super::short_link_preference::ShortLinkPreference;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Organization")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: String,
    pub description: Option<String>,
    #[sea_orm(column_name = "apiKey")]
    pub api_key: Option<String>,
    #[sea_orm(column_name = "paymentId")]
    pub payment_id: Option<String>,
    #[sea_orm(column_name = "streakSince")]
    pub streak_since: Option<DateTime>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "deletedAt")]
    pub deleted_at: Option<DateTime>,
    #[sea_orm(column_name = "allowTrial", default_value = false)]
    pub allow_trial: bool,
    #[sea_orm(column_name = "isTrailing", default_value = false)]
    pub is_trailing: bool,
    #[sea_orm(default_value = "ASK")]
    pub shortlink: ShortLinkPreference,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::auto_post::Entity")]
    AutoPost,
    #[sea_orm(has_many = "super::comments::Entity")]
    Comments,
    #[sea_orm(has_many = "super::credits::Entity")]
    Credits,
    #[sea_orm(has_many = "super::clipping::Entity")]
    Clippings,
    #[sea_orm(has_many = "super::customer::Entity")]
    Customers,
    #[sea_orm(has_many = "super::errors::Entity")]
    Errors,
    #[sea_orm(has_many = "super::git_hub::Entity")]
    Github,
    #[sea_orm(has_many = "super::integration::Entity")]
    Integration,
    #[sea_orm(has_many = "super::media::Entity")]
    Media,
    #[sea_orm(has_many = "super::messages_group::Entity")]
    BuyerOrganization,
    #[sea_orm(has_many = "super::notifications::Entity")]
    Notifications,
    #[sea_orm(has_many = "super::plugs::Entity")]
    Plugs,
    #[sea_orm(
        has_many = "super::post::Entity",
        from = "Column::Id",
        to = "super::post::Column::OrganizationId"
    )]
    Post,
    #[sea_orm(
        has_many = "super::post::Entity",
        from = "Column::Id",
        to = "super::post::Column::SubmittedForOrganizationId"
    )]
    SubmittedPost,
    #[sea_orm(has_many = "super::sets::Entity")]
    Sets,
    #[sea_orm(has_many = "super::signatures::Entity")]
    Signatures,
    #[sea_orm(has_many = "super::tags::Entity")]
    Tags,
    #[sea_orm(has_many = "super::third_party::Entity")]
    ThirdParty,
    #[sea_orm(has_many = "super::used_codes::Entity")]
    UsedCodes,
    #[sea_orm(has_many = "super::user_organization::Entity")]
    Users,
    #[sea_orm(has_many = "super::webhooks::Entity")]
    Webhooks,
    #[sea_orm(has_many = "super::oauth_app::Entity")]
    OauthApp,
    #[sea_orm(has_many = "super::oauth_authorization::Entity")]
    OauthAuthorizations,
    #[sea_orm(has_one = "super::subscription::Entity")]
    Subscription,
}

impl Related<super::auto_post::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AutoPost.def()
    }
}

impl Related<super::comments::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Comments.def()
    }
}

impl Related<super::credits::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Credits.def()
    }
}

impl Related<super::clipping::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Clippings.def()
    }
}

impl Related<super::customer::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Customers.def()
    }
}

impl Related<super::errors::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Errors.def()
    }
}

impl Related<super::git_hub::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Github.def()
    }
}

impl Related<super::integration::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Integration.def()
    }
}

impl Related<super::media::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Media.def()
    }
}

impl Related<super::messages_group::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BuyerOrganization.def()
    }
}

impl Related<super::notifications::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Notifications.def()
    }
}

impl Related<super::plugs::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Plugs.def()
    }
}

impl Related<super::post::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Post.def()
    }
}

impl Related<super::sets::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Sets.def()
    }
}

impl Related<super::signatures::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Signatures.def()
    }
}

impl Related<super::tags::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Tags.def()
    }
}

impl Related<super::third_party::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ThirdParty.def()
    }
}

impl Related<super::used_codes::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::UsedCodes.def()
    }
}

impl Related<super::user_organization::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Users.def()
    }
}

impl Related<super::webhooks::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Webhooks.def()
    }
}

impl Related<super::oauth_app::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OauthApp.def()
    }
}

impl Related<super::oauth_authorization::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OauthAuthorizations.def()
    }
}

impl Related<super::subscription::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Subscription.def()
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
