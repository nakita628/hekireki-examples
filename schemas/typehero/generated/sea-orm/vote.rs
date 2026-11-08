use super::vote_type::VoteType;
use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Vote")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    #[sea_orm(column_name = "userId")]
    pub user_id: String,
    #[sea_orm(column_name = "rootType")]
    pub root_type: VoteType,
    #[sea_orm(column_name = "challengeId")]
    pub challenge_id: Option<i32>,
    #[sea_orm(column_name = "commentId")]
    pub comment_id: Option<i32>,
    #[sea_orm(column_name = "submissionId")]
    pub submission_id: Option<i32>,
    #[sea_orm(column_name = "sharedSolutionId")]
    pub shared_solution_id: Option<i32>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::UserId",
        to = "super::user::Column::Id"
    )]
    User,
    #[sea_orm(
        belongs_to = "super::challenge::Entity",
        from = "Column::ChallengeId",
        to = "super::challenge::Column::Id"
    )]
    Challenge,
    #[sea_orm(
        belongs_to = "super::comment::Entity",
        from = "Column::CommentId",
        to = "super::comment::Column::Id"
    )]
    Comment,
    #[sea_orm(
        belongs_to = "super::submission::Entity",
        from = "Column::SubmissionId",
        to = "super::submission::Column::Id"
    )]
    Submission,
    #[sea_orm(
        belongs_to = "super::shared_solution::Entity",
        from = "Column::SharedSolutionId",
        to = "super::shared_solution::Column::Id"
    )]
    SharedSolution,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

impl Related<super::challenge::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Challenge.def()
    }
}

impl Related<super::comment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Comment.def()
    }
}

impl Related<super::submission::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Submission.def()
    }
}

impl Related<super::shared_solution::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SharedSolution.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
