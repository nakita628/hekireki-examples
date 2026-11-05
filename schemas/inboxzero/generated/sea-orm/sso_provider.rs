use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "ssoProvider")]
pub struct Model {
    #[sea_orm(column_name = "userId")]
    pub user_id: Option<String>,
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub issuer: String,
    #[sea_orm(column_name = "oidcConfig")]
    pub oidc_config: Option<String>,
    #[sea_orm(column_name = "samlConfig")]
    pub saml_config: Option<String>,
    #[sea_orm(column_name = "emailAccountId")]
    pub email_account_id: Option<String>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "providerId")]
    pub provider_id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: Option<String>,
    pub domain: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::UserId",
        to = "super::user::Column::Id",
        on_delete = "SetNull"
    )]
    User,
    #[sea_orm(
        belongs_to = "super::email_account::Entity",
        from = "Column::EmailAccountId",
        to = "super::email_account::Column::Id",
        on_delete = "Cascade"
    )]
    EmailAccount,
    #[sea_orm(
        belongs_to = "super::organization::Entity",
        from = "Column::OrganizationId",
        to = "super::organization::Column::Id",
        on_delete = "Cascade"
    )]
    Organization,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

impl Related<super::email_account::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EmailAccount.def()
    }
}

impl Related<super::organization::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organization.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
