use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "account")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "accountId")]
    pub account_id: String,
    #[sea_orm(column_name = "providerId")]
    pub provider_id: String,
    #[sea_orm(column_name = "userId")]
    pub user_id: String,
    #[sea_orm(column_name = "accessToken")]
    pub access_token: Option<String>,
    #[sea_orm(column_name = "refreshToken")]
    pub refresh_token: Option<String>,
    #[sea_orm(column_name = "idToken")]
    pub id_token: Option<String>,
    #[sea_orm(column_name = "accessTokenExpiresAt")]
    pub access_token_expires_at: Option<DateTime>,
    #[sea_orm(column_name = "refreshTokenExpiresAt")]
    pub refresh_token_expires_at: Option<DateTime>,
    pub scope: Option<String>,
    pub password: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
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
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
