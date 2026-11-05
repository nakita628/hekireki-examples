use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Session")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "sessionToken")]
    pub session_token: String,
    #[sea_orm(column_name = "userId")]
    pub user_id: String,
    pub expires: DateTime,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "emailOtp", default_value = false)]
    pub email_otp: bool,
    #[sea_orm(column_name = "emailOtpVersion", default_value = 0)]
    pub email_otp_version: i32,
    #[sea_orm(column_name = "ipAddress")]
    pub ip_address: Option<String>,
    #[sea_orm(column_name = "userAgent")]
    pub user_agent: Option<String>,
    #[sea_orm(column_name = "activeOrganizationId")]
    pub active_organization_id: Option<String>,
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
    #[sea_orm(
        belongs_to = "super::organization::Entity",
        from = "Column::ActiveOrganizationId",
        to = "super::organization::Column::Id",
        on_delete = "SetNull"
    )]
    ActiveOrganization,
    #[sea_orm(has_many = "super::oauth_refresh_token::Entity")]
    OauthRefreshTokenRecords,
    #[sea_orm(has_many = "super::oauth_access_token::Entity")]
    OauthAccessTokenRecords,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

impl Related<super::organization::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ActiveOrganization.def()
    }
}

impl Related<super::oauth_refresh_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OauthRefreshTokenRecords.def()
    }
}

impl Related<super::oauth_access_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OauthAccessTokenRecords.def()
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
