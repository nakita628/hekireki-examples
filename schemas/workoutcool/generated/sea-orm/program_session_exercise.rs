use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "program_session_exercises")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "sessionId")]
    pub session_id: String,
    #[sea_orm(column_name = "exerciseId")]
    pub exercise_id: String,
    pub order: i32,
    #[sea_orm(column_type = "Text")]
    pub instructions: String,
    #[sea_orm(column_name = "instructionsEn", column_type = "Text")]
    pub instructions_en: String,
    #[sea_orm(column_name = "instructionsEs", column_type = "Text")]
    pub instructions_es: String,
    #[sea_orm(column_name = "instructionsPt", column_type = "Text")]
    pub instructions_pt: String,
    #[sea_orm(column_name = "instructionsRu", column_type = "Text")]
    pub instructions_ru: String,
    #[sea_orm(column_name = "instructionsZhCn", column_type = "Text")]
    pub instructions_zh_cn: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::program_session::Entity",
        from = "Column::SessionId",
        to = "super::program_session::Column::Id",
        on_delete = "Cascade"
    )]
    Session,
    #[sea_orm(
        belongs_to = "super::exercise::Entity",
        from = "Column::ExerciseId",
        to = "super::exercise::Column::Id"
    )]
    Exercise,
    #[sea_orm(has_many = "super::program_suggested_set::Entity")]
    SuggestedSets,
}

impl Related<super::program_session::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Session.def()
    }
}

impl Related<super::exercise::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Exercise.def()
    }
}

impl Related<super::program_suggested_set::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SuggestedSets.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
