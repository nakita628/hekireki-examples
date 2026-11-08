use super::user_status::UserStatus;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "User")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: String,
    #[sea_orm(unique)]
    pub email: String,
    #[sea_orm(column_name = "emailVerified")]
    pub email_verified: Option<DateTime>,
    pub image: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(1000))", default_value = "")]
    pub bio: String,
    #[sea_orm(default_value = "ACTIVE")]
    pub status: UserStatus,
    #[sea_orm(column_name = "banReason", column_type = "Text")]
    pub ban_reason: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::account::Entity")]
    Accounts,
    #[sea_orm(has_many = "super::session::Entity")]
    Sessions,
    #[sea_orm(has_many = "super::challenge::Entity")]
    Challenge,
    #[sea_orm(has_many = "super::vote::Entity")]
    Vote,
    #[sea_orm(has_many = "super::submission::Entity")]
    Submission,
    #[sea_orm(has_many = "super::bookmark::Entity")]
    Bookmark,
    #[sea_orm(has_many = "super::shared_solution::Entity")]
    SharedSolution,
    #[sea_orm(has_many = "super::comment::Entity")]
    Comment,
    #[sea_orm(
        has_many = "super::report::Entity",
        from = "Column::Id",
        to = "super::report::Column::ReporterId"
    )]
    Reports,
    #[sea_orm(
        has_many = "super::report::Entity",
        from = "Column::Id",
        to = "super::report::Column::UserId"
    )]
    ReportsAbout,
    #[sea_orm(
        has_many = "super::report::Entity",
        from = "Column::Id",
        to = "super::report::Column::ModeratorId"
    )]
    Moderated,
    #[sea_orm(has_many = "super::image_upload::Entity")]
    ImageUpload,
    #[sea_orm(has_many = "super::beta_tokens::Entity")]
    BetaTokens,
    #[sea_orm(has_many = "super::donation::Entity")]
    Donations,
    #[sea_orm(has_many = "super::short_url::Entity")]
    ShortUrls,
    #[sea_orm(
        has_many = "super::notification::Entity",
        from = "Column::Id",
        to = "super::notification::Column::ToUserId"
    )]
    ToUserNotification,
    #[sea_orm(
        has_many = "super::notification::Entity",
        from = "Column::Id",
        to = "super::notification::Column::FromUserId"
    )]
    FromUserNotification,
}

impl Related<super::account::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Accounts.def()
    }
}

impl Related<super::session::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Sessions.def()
    }
}

impl Related<super::challenge::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Challenge.def()
    }
}

impl Related<super::vote::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Vote.def()
    }
}

impl Related<super::submission::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Submission.def()
    }
}

impl Related<super::bookmark::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Bookmark.def()
    }
}

impl Related<super::shared_solution::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SharedSolution.def()
    }
}

impl Related<super::comment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Comment.def()
    }
}

impl Related<super::report::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Reports.def()
    }
}

impl Related<super::image_upload::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ImageUpload.def()
    }
}

impl Related<super::beta_tokens::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BetaTokens.def()
    }
}

impl Related<super::donation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Donations.def()
    }
}

impl Related<super::short_url::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ShortUrls.def()
    }
}

impl Related<super::notification::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ToUserNotification.def()
    }
}

impl Related<super::role::Entity> for Entity {
    fn to() -> RelationDef {
        super::role_to_user::Relation::Role.def()
    }
    fn via() -> Option<RelationDef> {
        Some(super::role_to_user::Relation::User.def().rev())
    }
}

impl Related<super::user_link::Entity> for Entity {
    fn to() -> RelationDef {
        super::user_to_user_link::Relation::UserLink.def()
    }
    fn via() -> Option<RelationDef> {
        Some(super::user_to_user_link::Relation::User.def().rev())
    }
}

impl Related<super::track::Entity> for Entity {
    fn to() -> RelationDef {
        super::track_to_user::Relation::Track.def()
    }
    fn via() -> Option<RelationDef> {
        Some(super::track_to_user::Relation::User.def().rev())
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
