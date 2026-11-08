use super::report_status::ReportStatus;
use super::report_type::ReportType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Report")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    #[sea_orm(column_type = "Text")]
    pub text: String,
    pub r#type: ReportType,
    pub status: ReportStatus,
    #[sea_orm(column_name = "reporterId")]
    pub reporter_id: String,
    #[sea_orm(column_name = "userId")]
    pub user_id: Option<String>,
    #[sea_orm(column_name = "challengeId")]
    pub challenge_id: Option<i32>,
    #[sea_orm(column_name = "commentId")]
    pub comment_id: Option<i32>,
    #[sea_orm(column_name = "solutionId")]
    pub solution_id: Option<i32>,
    #[sea_orm(column_name = "moderatorId")]
    pub moderator_id: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::ReporterId",
        to = "super::user::Column::Id"
    )]
    Reporter,
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
        belongs_to = "super::shared_solution::Entity",
        from = "Column::SolutionId",
        to = "super::shared_solution::Column::Id"
    )]
    Solution,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::ModeratorId",
        to = "super::user::Column::Id"
    )]
    Moderator,
    #[sea_orm(has_many = "super::report_issue::Entity")]
    Issues,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Reporter.def()
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

impl Related<super::shared_solution::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Solution.def()
    }
}

impl Related<super::report_issue::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Issues.def()
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
