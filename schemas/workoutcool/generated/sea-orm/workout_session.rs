use super::exercise_attribute_value_enum::ExerciseAttributeValueEnum;
use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "workout_sessions")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "userId")]
    pub user_id: String,
    #[sea_orm(column_name = "startedAt")]
    pub started_at: DateTime,
    #[sea_orm(column_name = "endedAt")]
    pub ended_at: Option<DateTime>,
    pub duration: Option<i32>,
    pub muscles: Vec<ExerciseAttributeValueEnum>,
    pub rating: Option<i32>,
    #[sea_orm(column_name = "ratingComment", column_type = "Text")]
    pub rating_comment: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::UserId",
        to = "super::user::Column::Id"
    )]
    User,
    #[sea_orm(has_many = "super::workout_session_exercise::Entity")]
    Exercises,
    #[sea_orm(has_one = "super::user_session_progress::Entity")]
    UserSessionProgress,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

impl Related<super::workout_session_exercise::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Exercises.def()
    }
}

impl Related<super::user_session_progress::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::UserSessionProgress.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
