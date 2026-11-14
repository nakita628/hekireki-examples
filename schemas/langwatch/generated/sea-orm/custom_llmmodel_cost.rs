use super::custom_llmmodel_cost_scope_type::CustomLLMModelCostScopeType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "CustomLLMModelCost")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    #[sea_orm(column_name = "scopeType", default_value = "PROJECT")]
    pub scope_type: CustomLLMModelCostScopeType,
    #[sea_orm(column_name = "scopeId")]
    pub scope_id: String,
    #[sea_orm(column_name = "projectId")]
    pub project_id: Option<String>,
    pub model: String,
    pub regex: String,
    #[sea_orm(column_name = "inputCostPerToken")]
    pub input_cost_per_token: Option<f64>,
    #[sea_orm(column_name = "outputCostPerToken")]
    pub output_cost_per_token: Option<f64>,
    #[sea_orm(column_name = "cacheReadCostPerToken")]
    pub cache_read_cost_per_token: Option<f64>,
    #[sea_orm(column_name = "cacheCreationCostPerToken")]
    pub cache_creation_cost_per_token: Option<f64>,
    #[sea_orm(column_name = "cacheCreation1hCostPerToken")]
    pub cache_creation1h_cost_per_token: Option<f64>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

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
