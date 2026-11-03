use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "OAuthClient")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "clientId")]
    pub client_id: String,
    #[sea_orm(column_name = "clientName")]
    pub client_name: String,
    #[sea_orm(column_name = "clientUri")]
    pub client_uri: Option<String>,
    #[sea_orm(column_name = "logoUri")]
    pub logo_uri: Option<String>,
    #[sea_orm(column_name = "redirectUris")]
    pub redirect_uris: Vec<String>,
    #[sea_orm(column_name = "grantTypes")]
    pub grant_types: Vec<String>,
    pub scopes: String,
    #[sea_orm(column_name = "tokenAuthMethod")]
    pub token_auth_method: String,
    #[sea_orm(column_name = "hashedSecret")]
    pub hashed_secret: Option<String>,
    #[sea_orm(column_name = "registrationType")]
    pub registration_type: String,
    #[sea_orm(column_name = "metadataUrl")]
    pub metadata_url: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::restricted_token::Entity")]
    Tokens,
}

impl Related<super::restricted_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Tokens.def()
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
