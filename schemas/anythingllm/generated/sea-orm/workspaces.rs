use super::prisma_date_time::PrismaDateTime;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "workspaces")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    pub name: String,
    #[sea_orm(unique)]
    pub slug: String,
    #[sea_orm(column_name = "vectorTag")]
    pub vector_tag: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: PrismaDateTime,
    #[sea_orm(column_name = "openAiTemp")]
    pub open_ai_temp: Option<f64>,
    #[sea_orm(column_name = "openAiHistory", default_value = 20)]
    pub open_ai_history: i32,
    #[sea_orm(column_name = "lastUpdatedAt")]
    pub last_updated_at: PrismaDateTime,
    #[sea_orm(column_name = "openAiPrompt")]
    pub open_ai_prompt: Option<String>,
    #[sea_orm(column_name = "similarityThreshold", default_value = 0.25)]
    pub similarity_threshold: Option<f64>,
    #[sea_orm(column_name = "chatProvider")]
    pub chat_provider: Option<String>,
    #[sea_orm(column_name = "chatModel")]
    pub chat_model: Option<String>,
    #[sea_orm(column_name = "topN", default_value = 4)]
    pub top_n: Option<i32>,
    #[sea_orm(column_name = "chatMode", default_value = "chat")]
    pub chat_mode: Option<String>,
    #[sea_orm(column_name = "pfpFilename")]
    pub pfp_filename: Option<String>,
    #[sea_orm(column_name = "agentProvider")]
    pub agent_provider: Option<String>,
    #[sea_orm(column_name = "agentModel")]
    pub agent_model: Option<String>,
    #[sea_orm(column_name = "queryRefusalResponse")]
    pub query_refusal_response: Option<String>,
    #[sea_orm(column_name = "vectorSearchMode", default_value = "default")]
    pub vector_search_mode: Option<String>,
    pub router_id: Option<i32>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::workspace_users::Entity")]
    WorkspaceUsers,
    #[sea_orm(has_many = "super::workspace_documents::Entity")]
    Documents,
    #[sea_orm(has_many = "super::workspace_suggested_messages::Entity")]
    WorkspaceSuggestedMessages,
    #[sea_orm(has_many = "super::embed_configs::Entity")]
    EmbedConfigs,
    #[sea_orm(has_many = "super::workspace_threads::Entity")]
    Threads,
    #[sea_orm(has_many = "super::workspace_agent_invocations::Entity")]
    WorkspaceAgentInvocations,
    #[sea_orm(has_many = "super::prompt_history::Entity")]
    PromptHistory,
    #[sea_orm(has_many = "super::workspace_parsed_files::Entity")]
    WorkspaceParsedFiles,
    #[sea_orm(has_many = "super::memories::Entity")]
    Memories,
}

impl Related<super::workspace_users::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WorkspaceUsers.def()
    }
}

impl Related<super::workspace_documents::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Documents.def()
    }
}

impl Related<super::workspace_suggested_messages::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WorkspaceSuggestedMessages.def()
    }
}

impl Related<super::embed_configs::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EmbedConfigs.def()
    }
}

impl Related<super::workspace_threads::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Threads.def()
    }
}

impl Related<super::workspace_agent_invocations::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WorkspaceAgentInvocations.def()
    }
}

impl Related<super::prompt_history::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PromptHistory.def()
    }
}

impl Related<super::workspace_parsed_files::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WorkspaceParsedFiles.def()
    }
}

impl Related<super::memories::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Memories.def()
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
            self.created_at = Set(now.into());
        }
        if insert && self.last_updated_at.is_not_set() {
            self.last_updated_at = Set(now.into());
        }
        Ok(self)
    }
}
