use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "OidcProvider")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    pub name: String,
    pub issuer: String,
    #[sea_orm(column_name = "clientId")]
    pub client_id: String,
    #[sea_orm(column_name = "clientSecret")]
    pub client_secret: String,
    #[sea_orm(column_name = "authorizationEndpoint")]
    pub authorization_endpoint: Option<String>,
    #[sea_orm(column_name = "tokenEndpoint")]
    pub token_endpoint: Option<String>,
    #[sea_orm(column_name = "userinfoEndpoint")]
    pub userinfo_endpoint: Option<String>,
    #[sea_orm(column_name = "jwksUri")]
    pub jwks_uri: Option<String>,
    #[sea_orm(column_name = "introspectionEndpoint")]
    pub introspection_endpoint: Option<String>,
    pub scopes: Vec<String>,
    pub audience: Option<String>,
    pub prompt: Option<String>,
    #[sea_orm(column_name = "usernameClaim", default_value = "email")]
    pub username_claim: String,
    #[sea_orm(column_name = "emailClaim", default_value = "email")]
    pub email_claim: String,
    #[sea_orm(column_name = "nameClaim", default_value = "name")]
    pub name_claim: String,
    #[sea_orm(column_name = "groupClaim", default_value = "groups")]
    pub group_claim: String,
    #[sea_orm(default_value = true)]
    pub enabled: bool,
    #[sea_orm(column_name = "autoDiscovery", default_value = true)]
    pub auto_discovery: bool,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::oidc_login_group::Entity")]
    OidcLoginGroups,
}

impl Related<super::oidc_login_group::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OidcLoginGroups.def()
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
