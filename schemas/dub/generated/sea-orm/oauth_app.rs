use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "OAuthApp")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "integrationId")]
    pub integration_id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "clientId")]
    pub client_id: String,
    #[sea_orm(column_name = "hashedClientSecret")]
    pub hashed_client_secret: String,
    #[sea_orm(column_name = "partialClientSecret")]
    pub partial_client_secret: String,
    #[sea_orm(column_name = "redirectUris")]
    pub redirect_uris: Json,
    #[sea_orm(default_value = false)]
    pub pkce: bool,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::integration::Entity",
        from = "Column::IntegrationId",
        to = "super::integration::Column::Id",
        on_delete = "Cascade"
    )]
    Integration,
    #[sea_orm(has_many = "super::oauth_code::Entity")]
    OAuthCodes,
}

impl Related<super::integration::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Integration.def()
    }
}

impl Related<super::oauth_code::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OAuthCodes.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
