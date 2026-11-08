use super::workout_set_type::WorkoutSetType;
use super::workout_set_unit::WorkoutSetUnit;
use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "program_suggested_sets")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "programSessionExerciseId")]
    pub program_session_exercise_id: String,
    #[sea_orm(column_name = "setIndex")]
    pub set_index: i32,
    pub types: Vec<WorkoutSetType>,
    #[sea_orm(column_name = "valuesInt")]
    pub values_int: Vec<i32>,
    #[sea_orm(column_name = "valuesSec")]
    pub values_sec: Vec<i32>,
    pub units: Vec<WorkoutSetUnit>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::program_session_exercise::Entity",
        from = "Column::ProgramSessionExerciseId",
        to = "super::program_session_exercise::Column::Id",
        on_delete = "Cascade"
    )]
    ProgramSessionExercise,
}

impl Related<super::program_session_exercise::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ProgramSessionExercise.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
