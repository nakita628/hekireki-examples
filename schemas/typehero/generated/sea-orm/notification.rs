use super::notification_type::NotificationType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Notification")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "isRead", default_value = false)]
    pub is_read: bool,
    pub r#type: NotificationType,
    #[sea_orm(column_name = "toUserId")]
    pub to_user_id: String,
    #[sea_orm(column_name = "fromUserId")]
    pub from_user_id: String,
    #[sea_orm(column_name = "commentId")]
    pub comment_id: Option<i32>,
    pub content: Option<String>,
    pub blurb: Option<String>,
    pub url: String,
    #[sea_orm(column_name = "challengeId")]
    pub challenge_id: Option<i32>,
    #[sea_orm(column_name = "sharedSolutionId")]
    pub shared_solution_id: Option<i32>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::ToUserId",
        to = "super::user::Column::Id"
    )]
    ToUser,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::FromUserId",
        to = "super::user::Column::Id"
    )]
    FromUser,
    #[sea_orm(
        belongs_to = "super::comment::Entity",
        from = "Column::CommentId",
        to = "super::comment::Column::Id"
    )]
    Comment,
    #[sea_orm(
        belongs_to = "super::challenge::Entity",
        from = "Column::ChallengeId",
        to = "super::challenge::Column::Id"
    )]
    Challenge,
    #[sea_orm(
        belongs_to = "super::shared_solution::Entity",
        from = "Column::SharedSolutionId",
        to = "super::shared_solution::Column::Id"
    )]
    SharedSolution,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ToUser.def()
    }
}

impl Related<super::comment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Comment.def()
    }
}

impl Related<super::challenge::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Challenge.def()
    }
}

impl Related<super::shared_solution::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SharedSolution.def()
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
