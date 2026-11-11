use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_GuardrailsTable")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub guardrail_id: String,
    #[sea_orm(unique)]
    pub guardrail_name: String,
    pub litellm_params: Json,
    pub guardrail_info: Option<Json>,
    pub team_id: Option<String>,
    pub created_at: DateTime,
    pub updated_at: DateTime,
    #[sea_orm(default_value = "active")]
    pub status: String,
    pub submitted_at: Option<DateTime>,
    pub reviewed_at: Option<DateTime>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    fn new() -> Self {
        Self {
            guardrail_id: Set(uuid::Uuid::new_v4().to_string()),
            ..ActiveModelTrait::default()
        }
    }

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
