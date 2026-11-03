use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "SurveyLanguage")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "languageId")]
    pub language_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "surveyId")]
    pub survey_id: String,
    #[sea_orm(default_value = false)]
    pub default: bool,
    #[sea_orm(default_value = true)]
    pub enabled: bool,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::language::Entity",
        from = "Column::LanguageId",
        to = "super::language::Column::Id",
        on_delete = "Cascade"
    )]
    Language,
    #[sea_orm(
        belongs_to = "super::survey::Entity",
        from = "Column::SurveyId",
        to = "super::survey::Column::Id",
        on_delete = "Cascade"
    )]
    Survey,
}

impl Related<super::language::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Language.def()
    }
}

impl Related<super::survey::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Survey.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
