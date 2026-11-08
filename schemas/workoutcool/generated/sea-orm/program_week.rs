use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "program_weeks")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "programId")]
    pub program_id: String,
    #[sea_orm(column_name = "weekNumber")]
    pub week_number: i32,
    pub title: String,
    #[sea_orm(column_name = "titleEn")]
    pub title_en: String,
    #[sea_orm(column_name = "titleEs")]
    pub title_es: String,
    #[sea_orm(column_name = "titlePt")]
    pub title_pt: String,
    #[sea_orm(column_name = "titleRu")]
    pub title_ru: String,
    #[sea_orm(column_name = "titleZhCn")]
    pub title_zh_cn: String,
    #[sea_orm(column_type = "Text")]
    pub description: String,
    #[sea_orm(column_name = "descriptionEn", column_type = "Text")]
    pub description_en: String,
    #[sea_orm(column_name = "descriptionEs", column_type = "Text")]
    pub description_es: String,
    #[sea_orm(column_name = "descriptionPt", column_type = "Text")]
    pub description_pt: String,
    #[sea_orm(column_name = "descriptionRu", column_type = "Text")]
    pub description_ru: String,
    #[sea_orm(column_name = "descriptionZhCn", column_type = "Text")]
    pub description_zh_cn: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::program::Entity",
        from = "Column::ProgramId",
        to = "super::program::Column::Id",
        on_delete = "Cascade"
    )]
    Program,
    #[sea_orm(has_many = "super::program_session::Entity")]
    Sessions,
}

impl Related<super::program::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Program.def()
    }
}

impl Related<super::program_session::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Sessions.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
