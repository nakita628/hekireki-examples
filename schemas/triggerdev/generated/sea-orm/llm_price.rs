use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "llm_prices")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub model_id: String,
    pub pricing_tier_id: String,
    pub usage_type: String,
    #[sea_orm(column_type = "Decimal(Some((20, 12)))")]
    pub price: Decimal,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::llm_model::Entity",
        from = "Column::ModelId",
        to = "super::llm_model::Column::Id",
        on_delete = "Cascade"
    )]
    Model,
    #[sea_orm(
        belongs_to = "super::llm_pricing_tier::Entity",
        from = "Column::PricingTierId",
        to = "super::llm_pricing_tier::Column::Id",
        on_delete = "Cascade"
    )]
    PricingTier,
}

impl Related<super::llm_model::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Model.def()
    }
}

impl Related<super::llm_pricing_tier::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PricingTier.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
