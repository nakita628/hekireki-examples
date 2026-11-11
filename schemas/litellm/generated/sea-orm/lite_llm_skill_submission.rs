use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_SkillSubmission")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub plugin_name: String,
    pub operation: String,
    #[sea_orm(default_value = "pending_review")]
    pub status: String,
    pub proposal_json: String,
    pub archive_content: Option<Vec<u8>>,
    pub submitted_by: String,
    pub created_at: DateTime,
    pub base_plugin_id: Option<String>,
    pub base_updated_at: Option<DateTime>,
    pub reviewed_by: Option<String>,
    pub reviewed_at: Option<DateTime>,
    pub rejection_reason: Option<String>,
    pub scan_json: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    fn new() -> Self {
        Self {
            id: Set(uuid::Uuid::new_v4().to_string()),
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
        Ok(self)
    }
}
