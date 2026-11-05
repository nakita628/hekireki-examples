use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "users")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    pub email: String,
    #[sea_orm(column_name = "firstName")]
    pub first_name: Option<String>,
    #[sea_orm(column_name = "lastName")]
    pub last_name: Option<String>,
    pub name: Option<String>,
    #[sea_orm(column_name = "emailVerified", default_value = false)]
    pub email_verified: bool,
    pub image: Option<String>,
    #[sea_orm(column_name = "twoFactorEnabled", default_value = false)]
    pub two_factor_enabled: bool,
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
        has_many = "super::organization::Entity",
        from = "Column::Id",
        to = "super::organization::Column::CreatedByUserId"
    )]
    CreatedOrganizations,
    #[sea_orm(
        has_many = "super::organization::Entity",
        from = "Column::Id",
        to = "super::organization::Column::SubscriptionCreatedByUserId"
    )]
    Subscriptions,
    #[sea_orm(
        has_many = "super::member::Entity",
        from = "Column::Id",
        to = "super::member::Column::UserId"
    )]
    Membership,
    #[sea_orm(
        has_many = "super::member::Entity",
        from = "Column::Id",
        to = "super::member::Column::InvitedById"
    )]
    SentInvites,
    #[sea_orm(has_many = "super::session::Entity")]
    Sessions,
    #[sea_orm(has_many = "super::account::Entity")]
    Accounts,
    #[sea_orm(has_many = "super::invite::Entity")]
    Invites,
    #[sea_orm(has_many = "super::conversation::Entity")]
    Conversations,
    #[sea_orm(has_many = "super::two_factor_challenge::Entity")]
    TwoFactorChallenges,
    #[sea_orm(has_one = "super::two_factor::Entity")]
    TwoFactor,
    #[sea_orm(has_one = "super::user_totp::Entity")]
    Totp,
}

impl Related<super::organization::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CreatedOrganizations.def()
    }
}

impl Related<super::member::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Membership.def()
    }
}

impl Related<super::session::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Sessions.def()
    }
}

impl Related<super::account::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Accounts.def()
    }
}

impl Related<super::invite::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Invites.def()
    }
}

impl Related<super::conversation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Conversations.def()
    }
}

impl Related<super::two_factor_challenge::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TwoFactorChallenges.def()
    }
}

impl Related<super::two_factor::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TwoFactor.def()
    }
}

impl Related<super::user_totp::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Totp.def()
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
