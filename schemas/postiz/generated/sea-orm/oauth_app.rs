use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "OAuthApp")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: Option<String>,
    pub name: String,
    pub description: Option<String>,
    #[sea_orm(column_name = "pictureId")]
    pub picture_id: Option<String>,
    #[sea_orm(column_name = "redirectUrl")]
    pub redirect_url: String,
    #[sea_orm(column_name = "redirectUris")]
    pub redirect_uris: Option<String>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "clientId")]
    pub client_id: String,
    #[sea_orm(column_name = "clientSecret")]
    pub client_secret: Option<String>,
    #[sea_orm(default_value = false)]
    pub dynamic: bool,
    #[sea_orm(column_name = "tokenEndpointAuthMethod")]
    pub token_endpoint_auth_method: Option<String>,
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
        belongs_to = "super::organization::Entity",
        from = "Column::OrganizationId",
        to = "super::organization::Column::Id"
    )]
    Organization,
    #[sea_orm(
        belongs_to = "super::media::Entity",
        from = "Column::PictureId",
        to = "super::media::Column::Id"
    )]
    Picture,
    #[sea_orm(has_many = "super::oauth_authorization::Entity")]
    Authorizations,
    #[sea_orm(has_many = "super::oauth_self_hosted_authorization::Entity")]
    SelfHostedAuthorizations,
}

impl Related<super::organization::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organization.def()
    }
}

impl Related<super::media::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Picture.def()
    }
}

impl Related<super::oauth_authorization::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Authorizations.def()
    }
}

impl Related<super::oauth_self_hosted_authorization::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SelfHostedAuthorizations.def()
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
