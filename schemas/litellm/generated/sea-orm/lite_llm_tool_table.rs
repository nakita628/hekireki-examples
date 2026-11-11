use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_ToolTable")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub tool_id: String,
    #[sea_orm(unique)]
    pub tool_name: String,
    pub origin: Option<String>,
    #[sea_orm(default_value = "untrusted")]
    pub input_policy: String,
    #[sea_orm(default_value = "untrusted")]
    pub output_policy: String,
    #[sea_orm(default_value = 0)]
    pub call_count: i32,
    #[sea_orm(default_value = "{}")]
    pub assignments: Option<Json>,
    pub key_hash: Option<String>,
    pub team_id: Option<String>,
    pub key_alias: Option<String>,
    pub user_agent: Option<String>,
    pub last_used_at: Option<DateTime>,
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
            tool_id: Set(uuid::Uuid::new_v4().to_string()),
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
