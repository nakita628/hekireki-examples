use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_AccessGroupTable")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub access_group_id: String,
    #[sea_orm(unique)]
    pub access_group_name: String,
    pub description: Option<String>,
    pub access_model_names: Vec<String>,
    pub access_mcp_server_ids: Vec<String>,
    pub mcp_tool_permissions: Option<Json>,
    pub access_agent_ids: Vec<String>,
    pub access_skill_names: Vec<String>,
    pub access_vector_store_ids: Vec<String>,
    pub assigned_team_ids: Vec<String>,
    pub assigned_key_ids: Vec<String>,
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
            access_group_id: Set(uuid::Uuid::new_v4().to_string()),
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
