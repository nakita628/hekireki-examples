use super::workout_set_type::WorkoutSetType;
use super::workout_set_unit::WorkoutSetUnit;
use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "workout_sets")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "workoutSessionExerciseId")]
    pub workout_session_exercise_id: String,
    #[sea_orm(column_name = "setIndex")]
    pub set_index: i32,
    pub r#type: WorkoutSetType,
    pub types: Vec<WorkoutSetType>,
    #[sea_orm(column_name = "valuesInt")]
    pub values_int: Vec<i32>,
    #[sea_orm(column_name = "valuesSec")]
    pub values_sec: Vec<i32>,
    pub units: Vec<WorkoutSetUnit>,
    #[sea_orm(default_value = false)]
    pub completed: bool,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::workout_session_exercise::Entity",
        from = "Column::WorkoutSessionExerciseId",
        to = "super::workout_session_exercise::Column::Id",
        on_delete = "Cascade"
    )]
    WorkoutSessionExercise,
}

impl Related<super::workout_session_exercise::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WorkoutSessionExercise.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
