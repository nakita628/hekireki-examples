use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Account")]
pub struct Model {
    pub password: Option<String>,
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "userId")]
    pub user_id: String,
    pub provider: String,
    #[sea_orm(default_value = "oidc")]
    pub r#type: String,
    #[sea_orm(column_name = "providerAccountId")]
    pub provider_account_id: String,
    #[sea_orm(column_type = "Text")]
    pub refresh_token: Option<String>,
    #[sea_orm(column_name = "refreshTokenExpiresAt")]
    pub refresh_token_expires_at: Option<DateTime>,
    #[sea_orm(column_type = "Text")]
    pub access_token: Option<String>,
    pub expires_at: Option<DateTime>,
    #[sea_orm(column_name = "disconnectedAt")]
    pub disconnected_at: Option<DateTime>,
    pub token_type: Option<String>,
    pub scope: Option<String>,
    #[sea_orm(column_type = "Text")]
    pub id_token: Option<String>,
    pub session_state: Option<String>,
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
    #[sea_orm(has_one = "super::email_account::Entity")]
    EmailAccount,
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
        if insert && self.expires_at.is_not_set() {
            self.expires_at = Set(Some(now.naive_utc()));
        }
        Ok(self)
    }
}
