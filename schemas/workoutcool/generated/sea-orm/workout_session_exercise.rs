use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "workout_session_exercises")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "workoutSessionId")]
    pub workout_session_id: String,
    #[sea_orm(column_name = "exerciseId")]
    pub exercise_id: String,
    pub order: i32,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::workout_session::Entity",
        from = "Column::WorkoutSessionId",
        to = "super::workout_session::Column::Id",
        on_delete = "Cascade"
    )]
    WorkoutSession,
    #[sea_orm(
        belongs_to = "super::exercise::Entity",
        from = "Column::ExerciseId",
        to = "super::exercise::Column::Id"
    )]
    Exercise,
    #[sea_orm(has_many = "super::workout_set::Entity")]
    Sets,
}

impl Related<super::workout_session::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WorkoutSession.def()
    }
}

impl Related<super::exercise::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Exercise.def()
    }
}

impl Related<super::workout_set::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Sets.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
