use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_LensReview")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub lens_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub criteria_key: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub execution_id: String,
    pub data: Json,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::lite_llm_lens::Entity",
        from = "Column::LensId",
        to = "super::lite_llm_lens::Column::Id",
        on_delete = "Cascade"
    )]
    Lens,
}

impl Related<super::lite_llm_lens::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Lens.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
