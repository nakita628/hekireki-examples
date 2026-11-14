use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "SsoProvider")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub issuer: String,
    #[sea_orm(column_name = "oidcConfig")]
    pub oidc_config: Option<String>,
    #[sea_orm(column_name = "samlConfig")]
    pub saml_config: Option<String>,
    #[sea_orm(column_name = "userId")]
    pub user_id: Option<String>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "providerId")]
    pub provider_id: String,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: Option<String>,
    pub domain: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

impl ActiveModelBehavior for ActiveModel {}
