use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "user_session_progress")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "enrollmentId")]
    pub enrollment_id: String,
    #[sea_orm(column_name = "sessionId")]
    pub session_id: String,
    #[sea_orm(column_name = "startedAt")]
    pub started_at: DateTime,
    #[sea_orm(column_name = "completedAt")]
    pub completed_at: Option<DateTime>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "workoutSessionId")]
    pub workout_session_id: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user_program_enrollment::Entity",
        from = "Column::EnrollmentId",
        to = "super::user_program_enrollment::Column::Id",
        on_delete = "Cascade"
    )]
    Enrollment,
    #[sea_orm(
        belongs_to = "super::program_session::Entity",
        from = "Column::SessionId",
        to = "super::program_session::Column::Id"
    )]
    Session,
    #[sea_orm(
        belongs_to = "super::workout_session::Entity",
        from = "Column::WorkoutSessionId",
        to = "super::workout_session::Column::Id"
    )]
    WorkoutSession,
}

impl Related<super::user_program_enrollment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Enrollment.def()
    }
}

impl Related<super::program_session::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Session.def()
    }
}

impl Related<super::workout_session::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WorkoutSession.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.started_at.is_not_set() {
            self.started_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
