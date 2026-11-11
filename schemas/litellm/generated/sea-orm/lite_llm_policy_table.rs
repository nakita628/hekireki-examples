use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_PolicyTable")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub policy_id: String,
    pub policy_name: String,
    #[sea_orm(default_value = 1)]
    pub version_number: i32,
    #[sea_orm(default_value = "production")]
    pub version_status: String,
    pub parent_version_id: Option<String>,
    #[sea_orm(default_value = true)]
    pub is_latest: bool,
    pub published_at: Option<DateTime>,
    pub production_at: Option<DateTime>,
    pub inherit: Option<String>,
    pub description: Option<String>,
    pub guardrails_add: Vec<String>,
    pub guardrails_remove: Vec<String>,
    #[sea_orm(default_value = "{}")]
    pub condition: Option<Json>,
    pub pipeline: Option<Json>,
    pub created_at: DateTime,
    pub created_by: Option<String>,
    pub updated_at: DateTime,
    pub updated_by: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    fn new() -> Self {
        Self {
            policy_id: Set(uuid::Uuid::new_v4().to_string()),
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
