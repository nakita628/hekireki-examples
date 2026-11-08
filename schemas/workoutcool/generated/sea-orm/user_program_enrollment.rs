use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "user_program_enrollments")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "userId")]
    pub user_id: String,
    #[sea_orm(column_name = "programId")]
    pub program_id: String,
    #[sea_orm(column_name = "enrolledAt")]
    pub enrolled_at: DateTime,
    #[sea_orm(column_name = "currentWeek", default_value = 1)]
    pub current_week: i32,
    #[sea_orm(column_name = "currentSession", default_value = 1)]
    pub current_session: i32,
    #[sea_orm(column_name = "completedSessions", default_value = 0)]
    pub completed_sessions: i32,
    #[sea_orm(column_name = "isActive", default_value = true)]
    pub is_active: bool,
    #[sea_orm(column_name = "completedAt")]
    pub completed_at: Option<DateTime>,
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
        belongs_to = "super::program::Entity",
        from = "Column::ProgramId",
        to = "super::program::Column::Id"
    )]
    Program,
    #[sea_orm(has_many = "super::user_session_progress::Entity")]
    SessionProgress,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

impl Related<super::program::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Program.def()
    }
}

impl Related<super::user_session_progress::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SessionProgress.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.enrolled_at.is_not_set() {
            self.enrolled_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
