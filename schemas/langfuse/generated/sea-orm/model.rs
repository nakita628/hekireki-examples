use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "models")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub created_at: DateTime,
    pub updated_at: DateTime,
    pub project_id: Option<String>,
    pub model_name: String,
    pub match_pattern: String,
    pub start_date: Option<DateTime>,
    pub input_price: Option<Decimal>,
    pub output_price: Option<Decimal>,
    pub total_price: Option<Decimal>,
    pub unit: Option<String>,
    pub tokenizer_id: Option<String>,
    pub tokenizer_config: Option<Json>,
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
    #[sea_orm(has_many = "super::price::Entity")]
    Price,
    #[sea_orm(has_many = "super::pricing_tier::Entity")]
    PricingTiers,
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl Related<super::price::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Price.def()
    }
}

impl Related<super::pricing_tier::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PricingTiers.def()
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
