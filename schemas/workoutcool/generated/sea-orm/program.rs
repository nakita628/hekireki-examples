use super::exercise_attribute_value_enum::ExerciseAttributeValueEnum;
use super::program_level::ProgramLevel;
use super::program_visibility::ProgramVisibility;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "programs")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    pub slug: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "slugEn")]
    pub slug_en: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "slugEs")]
    pub slug_es: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "slugPt")]
    pub slug_pt: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "slugRu")]
    pub slug_ru: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "slugZhCn")]
    pub slug_zh_cn: String,
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
    pub category: String,
    pub image: String,
    pub level: ProgramLevel,
    pub r#type: ExerciseAttributeValueEnum,
    #[sea_orm(column_name = "durationWeeks", default_value = 4)]
    pub duration_weeks: i32,
    #[sea_orm(column_name = "sessionsPerWeek", default_value = 3)]
    pub sessions_per_week: i32,
    #[sea_orm(column_name = "sessionDurationMin", default_value = 30)]
    pub session_duration_min: i32,
    pub equipment: Vec<ExerciseAttributeValueEnum>,
    #[sea_orm(column_name = "isPremium", default_value = true)]
    pub is_premium: bool,
    #[sea_orm(column_name = "isActive", default_value = true)]
    pub is_active: bool,
    #[sea_orm(default_value = "DRAFT")]
    pub visibility: ProgramVisibility,
    #[sea_orm(column_name = "participantCount", default_value = 0)]
    pub participant_count: i32,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::program_week::Entity")]
    Weeks,
    #[sea_orm(has_many = "super::user_program_enrollment::Entity")]
    Enrollments,
    #[sea_orm(has_many = "super::program_coach::Entity")]
    Coaches,
}

impl Related<super::program_week::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Weeks.def()
    }
}

impl Related<super::user_program_enrollment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Enrollments.def()
    }
}

impl Related<super::program_coach::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Coaches.def()
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
