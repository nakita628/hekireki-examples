use super::evaluator_block_reason::EvaluatorBlockReason;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "topic_configs")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub created_at: DateTime,
    pub updated_at: DateTime,
    pub created_by: Option<String>,
    pub updated_by: Option<String>,
    #[sea_orm(unique)]
    pub project_id: String,
    #[sea_orm(default_value = false)]
    pub enabled: bool,
    pub blocked_at: Option<DateTime>,
    pub block_reason: Option<EvaluatorBlockReason>,
    pub block_message: Option<String>,
    pub summary_llm_api_key_id: Option<String>,
    pub summary_model: Option<String>,
    pub embedding_llm_api_key_id: Option<String>,
    pub embedding_model: Option<String>,
    #[sea_orm(default_value = 1024)]
    pub embedding_dimensions: i32,
    pub clustering_llm_api_key_id: Option<String>,
    pub clustering_model: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::CreatedBy",
        to = "super::user::Column::Id",
        on_delete = "SetNull"
    )]
    CreatedByUser,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::UpdatedBy",
        to = "super::user::Column::Id",
        on_delete = "SetNull"
    )]
    UpdatedByUser,
    #[sea_orm(
        belongs_to = "super::project::Entity",
        from = "Column::ProjectId",
        to = "super::project::Column::Id",
        on_delete = "Cascade"
    )]
    Project,
    #[sea_orm(
        belongs_to = "super::llm_api_keys::Entity",
        from = "Column::SummaryLlmApiKeyId",
        to = "super::llm_api_keys::Column::Id",
        on_delete = "SetNull"
    )]
    SummaryLlmApiKey,
    #[sea_orm(
        belongs_to = "super::llm_api_keys::Entity",
        from = "Column::EmbeddingLlmApiKeyId",
        to = "super::llm_api_keys::Column::Id",
        on_delete = "SetNull"
    )]
    EmbeddingLlmApiKey,
    #[sea_orm(
        belongs_to = "super::llm_api_keys::Entity",
        from = "Column::ClusteringLlmApiKeyId",
        to = "super::llm_api_keys::Column::Id",
        on_delete = "SetNull"
    )]
    ClusteringLlmApiKey,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CreatedByUser.def()
    }
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl Related<super::llm_api_keys::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SummaryLlmApiKey.def()
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
