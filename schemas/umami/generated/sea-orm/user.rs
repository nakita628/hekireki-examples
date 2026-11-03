use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "user")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "user_id", column_type = "Uuid")]
    pub id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_type = "String(StringLen::N(255))")]
    pub username: String,
    #[sea_orm(column_type = "String(StringLen::N(60))")]
    pub password: String,
    #[sea_orm(column_type = "String(StringLen::N(50))")]
    pub role: String,
    #[sea_orm(column_type = "String(StringLen::N(2183))")]
    pub logo_url: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(255))")]
    pub display_name: Option<String>,
    #[sea_orm(default_value = false)]
    pub two_factor_required: bool,
    #[sea_orm(column_type = "TimestampWithTimeZone")]
    pub created_at: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_type = "TimestampWithTimeZone")]
    pub updated_at: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_type = "TimestampWithTimeZone")]
    pub deleted_at: Option<DateTimeWithTimeZone>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        has_many = "super::website::Entity",
        from = "Column::Id",
        to = "super::website::Column::UserId"
    )]
    Websites,
    #[sea_orm(
        has_many = "super::website::Entity",
        from = "Column::Id",
        to = "super::website::Column::CreatedBy"
    )]
    CreatedBy,
    #[sea_orm(has_many = "super::link::Entity")]
    Links,
    #[sea_orm(has_many = "super::pixel::Entity")]
    Pixels,
    #[sea_orm(has_many = "super::team_user::Entity")]
    Teams,
    #[sea_orm(has_many = "super::report::Entity")]
    Reports,
    #[sea_orm(has_many = "super::board::Entity")]
    Boards,
    #[sea_orm(has_many = "super::two_factor_backup_code::Entity")]
    TwoFactorBackupCodes,
    #[sea_orm(has_many = "super::two_factor_otp_used::Entity")]
    TwoFactorOtpUseds,
    #[sea_orm(has_many = "super::api_key::Entity")]
    ApiKeys,
    #[sea_orm(has_one = "super::two_factor_auth::Entity")]
    TwoFactorAuth,
    #[sea_orm(has_one = "super::two_factor_rate_limit::Entity")]
    TwoFactorRateLimit,
}

impl Related<super::website::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Websites.def()
    }
}

impl Related<super::link::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Links.def()
    }
}

impl Related<super::pixel::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Pixels.def()
    }
}

impl Related<super::team_user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Teams.def()
    }
}

impl Related<super::report::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Reports.def()
    }
}

impl Related<super::board::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Boards.def()
    }
}

impl Related<super::two_factor_backup_code::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TwoFactorBackupCodes.def()
    }
}

impl Related<super::two_factor_otp_used::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TwoFactorOtpUseds.def()
    }
}

impl Related<super::api_key::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ApiKeys.def()
    }
}

impl Related<super::two_factor_auth::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TwoFactorAuth.def()
    }
}

impl Related<super::two_factor_rate_limit::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TwoFactorRateLimit.def()
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
            self.created_at = Set(Some(now.fixed_offset()));
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(Some(now.fixed_offset()));
        }
        Ok(self)
    }
}
