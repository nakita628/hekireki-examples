use super::comment_root::CommentRoot;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Comment")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    #[sea_orm(column_type = "Text")]
    pub text: String,
    #[sea_orm(default_value = true)]
    pub visible: bool,
    #[sea_orm(column_name = "parentId")]
    pub parent_id: Option<i32>,
    #[sea_orm(column_name = "userId")]
    pub user_id: String,
    #[sea_orm(column_name = "rootType", default_value = "CHALLENGE")]
    pub root_type: CommentRoot,
    #[sea_orm(column_name = "rootChallengeId")]
    pub root_challenge_id: Option<i32>,
    #[sea_orm(column_name = "rootSolutionId")]
    pub root_solution_id: Option<i32>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::comment::Entity",
        from = "Column::ParentId",
        to = "super::comment::Column::Id",
        on_update = "NoAction",
        on_delete = "NoAction"
    )]
    ParentComment,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::UserId",
        to = "super::user::Column::Id"
    )]
    User,
    #[sea_orm(
        belongs_to = "super::challenge::Entity",
        from = "Column::RootChallengeId",
        to = "super::challenge::Column::Id"
    )]
    RootChallenge,
    #[sea_orm(
        belongs_to = "super::shared_solution::Entity",
        from = "Column::RootSolutionId",
        to = "super::shared_solution::Column::Id"
    )]
    RootSolution,
    #[sea_orm(
        has_many = "super::comment::Entity",
        from = "Column::Id",
        to = "super::comment::Column::ParentId"
    )]
    Replies,
    #[sea_orm(has_many = "super::report::Entity")]
    Report,
    #[sea_orm(has_many = "super::vote::Entity")]
    Vote,
    #[sea_orm(has_many = "super::notification::Entity")]
    Notification,
}

impl Related<super::comment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ParentComment.def()
    }
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

impl Related<super::challenge::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RootChallenge.def()
    }
}

impl Related<super::shared_solution::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RootSolution.def()
    }
}

impl Related<super::report::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Report.def()
    }
}

impl Related<super::vote::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Vote.def()
    }
}

impl Related<super::notification::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Notification.def()
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
