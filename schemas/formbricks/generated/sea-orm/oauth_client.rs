use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "oauthClient")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "clientId")]
    pub client_id: String,
    #[sea_orm(column_name = "clientSecret")]
    pub client_secret: Option<String>,
    #[sea_orm(default_value = false)]
    pub disabled: Option<bool>,
    #[sea_orm(column_name = "skipConsent")]
    pub skip_consent: Option<bool>,
    #[sea_orm(column_name = "enableEndSession")]
    pub enable_end_session: Option<bool>,
    #[sea_orm(column_name = "subjectType")]
    pub subject_type: Option<String>,
    pub scopes: Vec<String>,
    #[sea_orm(column_name = "userId")]
    pub user_id: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: Option<DateTime>,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: Option<DateTime>,
    pub name: Option<String>,
    pub uri: Option<String>,
    pub icon: Option<String>,
    pub contacts: Vec<String>,
    pub tos: Option<String>,
    pub policy: Option<String>,
    #[sea_orm(column_name = "softwareId")]
    pub software_id: Option<String>,
    #[sea_orm(column_name = "softwareVersion")]
    pub software_version: Option<String>,
    #[sea_orm(column_name = "softwareStatement")]
    pub software_statement: Option<String>,
    #[sea_orm(column_name = "redirectUris")]
    pub redirect_uris: Vec<String>,
    #[sea_orm(column_name = "postLogoutRedirectUris")]
    pub post_logout_redirect_uris: Vec<String>,
    #[sea_orm(column_name = "tokenEndpointAuthMethod")]
    pub token_endpoint_auth_method: Option<String>,
    #[sea_orm(column_name = "grantTypes")]
    pub grant_types: Vec<String>,
    #[sea_orm(column_name = "responseTypes")]
    pub response_types: Vec<String>,
    pub public: Option<bool>,
    pub r#type: Option<String>,
    #[sea_orm(column_name = "requirePKCE")]
    pub require_pkce: Option<bool>,
    #[sea_orm(column_name = "referenceId")]
    pub reference_id: Option<String>,
    pub metadata: Option<Json>,
    #[sea_orm(column_name = "clientDiscoveryId")]
    pub client_discovery_id: Option<String>,
    #[sea_orm(column_name = "clientCredentialsScopes")]
    pub client_credentials_scopes: Vec<String>,
    #[sea_orm(column_name = "backchannelLogoutUri")]
    pub backchannel_logout_uri: Option<String>,
    #[sea_orm(column_name = "backchannelLogoutSessionRequired")]
    pub backchannel_logout_session_required: Option<bool>,
    #[sea_orm(column_name = "applicationType")]
    pub application_type: Option<String>,
    pub jwks: Option<String>,
    #[sea_orm(column_name = "jwksUri")]
    pub jwks_uri: Option<String>,
    #[sea_orm(column_name = "dpopBoundAccessTokens", default_value = false)]
    pub dpop_bound_access_tokens: Option<bool>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::UserId",
        to = "super::user::Column::Id",
        on_delete = "Cascade"
    )]
    User,
    #[sea_orm(has_many = "super::oauth_access_token::Entity")]
    AccessTokens,
    #[sea_orm(has_many = "super::oauth_refresh_token::Entity")]
    RefreshTokens,
    #[sea_orm(has_many = "super::oauth_consent::Entity")]
    Consents,
    #[sea_orm(has_many = "super::oauth_client_resource::Entity")]
    Resources,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

impl Related<super::oauth_access_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AccessTokens.def()
    }
}

impl Related<super::oauth_refresh_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RefreshTokens.def()
    }
}

impl Related<super::oauth_consent::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Consents.def()
    }
}

impl Related<super::oauth_client_resource::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Resources.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
