use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "PlatformOAuthClient")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: String,
    pub secret: String,
    pub permissions: i32,
    pub logo: Option<String>,
    #[sea_orm(column_name = "redirectUris")]
    pub redirect_uris: Vec<String>,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: i32,
    #[sea_orm(column_name = "bookingRedirectUri")]
    pub booking_redirect_uri: Option<String>,
    #[sea_orm(column_name = "bookingCancelRedirectUri")]
    pub booking_cancel_redirect_uri: Option<String>,
    #[sea_orm(column_name = "bookingRescheduleRedirectUri")]
    pub booking_reschedule_redirect_uri: Option<String>,
    #[sea_orm(column_name = "areEmailsEnabled", default_value = false)]
    pub are_emails_enabled: bool,
    #[sea_orm(column_name = "areDefaultEventTypesEnabled", default_value = true)]
    pub are_default_event_types_enabled: bool,
    #[sea_orm(column_name = "areCalendarEventsEnabled", default_value = true)]
    pub are_calendar_events_enabled: bool,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::team::Entity",
        from = "Column::OrganizationId",
        to = "super::team::Column::Id",
        on_delete = "Cascade"
    )]
    Organization,
    #[sea_orm(
        has_many = "super::team::Entity",
        from = "Column::Id",
        to = "super::team::Column::CreatedByOauthClientId"
    )]
    Teams,
    #[sea_orm(has_many = "super::access_token::Entity")]
    AccessTokens,
    #[sea_orm(has_many = "super::refresh_token::Entity")]
    RefreshToken,
    #[sea_orm(has_many = "super::platform_authorization_token::Entity")]
    AuthorizationTokens,
    #[sea_orm(has_many = "super::webhook::Entity")]
    Webhook,
}

impl Related<super::team::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organization.def()
    }
}

impl Related<super::access_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AccessTokens.def()
    }
}

impl Related<super::refresh_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RefreshToken.def()
    }
}

impl Related<super::platform_authorization_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AuthorizationTokens.def()
    }
}

impl Related<super::webhook::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Webhook.def()
    }
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        super::platform_oauth_client_to_user::Relation::User.def()
    }
    fn via() -> Option<RelationDef> {
        Some(
            super::platform_oauth_client_to_user::Relation::PlatformOAuthClient
                .def()
                .rev(),
        )
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
