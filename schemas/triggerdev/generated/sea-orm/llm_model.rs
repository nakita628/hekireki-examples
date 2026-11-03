use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "llm_models")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    pub friendly_id: String,
    pub project_id: Option<String>,
    pub model_name: String,
    pub match_pattern: String,
    pub start_date: Option<DateTime>,
    #[sea_orm(default_value = "default")]
    pub source: String,
    pub created_at: DateTime,
    pub updated_at: DateTime,
    pub provider: Option<String>,
    pub description: Option<String>,
    pub context_window: Option<i32>,
    pub max_output_tokens: Option<i32>,
    pub capabilities: Vec<String>,
    #[sea_orm(default_value = false)]
    pub is_hidden: bool,
    pub base_model_name: Option<String>,
    pub pricing_unit: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::project::Entity",
        from = "Column::ProjectId",
        to = "super::project::Column::Id",
        on_delete = "Cascade"
    )]
    Project,
    #[sea_orm(has_many = "super::llm_pricing_tier::Entity")]
    PricingTiers,
    #[sea_orm(has_many = "super::llm_price::Entity")]
    Prices,
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl Related<super::llm_pricing_tier::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PricingTiers.def()
    }
}

impl Related<super::llm_price::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Prices.def()
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
