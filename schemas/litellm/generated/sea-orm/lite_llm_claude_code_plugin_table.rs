use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_ClaudeCodePluginTable")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    pub name: String,
    pub version: Option<String>,
    pub description: Option<String>,
    pub manifest_json: Option<String>,
    #[sea_orm(default_value = "{}")]
    pub files_json: Option<String>,
    #[sea_orm(default_value = true)]
    pub enabled: bool,
    pub created_at: Option<DateTime>,
    pub updated_at: Option<DateTime>,
    pub created_by: Option<String>,
    #[sea_orm(default_value = "0")]
    pub anonymous_download_count: i64,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::lite_llm_claude_code_plugin_version::Entity")]
    Versions,
    #[sea_orm(has_many = "super::lite_llm_claude_code_plugin_download::Entity")]
    Downloads,
    #[sea_orm(has_one = "super::lite_llm_claude_code_plugin_upload::Entity")]
    UploadedArchive,
}

impl Related<super::lite_llm_claude_code_plugin_version::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Versions.def()
    }
}

impl Related<super::lite_llm_claude_code_plugin_download::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Downloads.def()
    }
}

impl Related<super::lite_llm_claude_code_plugin_upload::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::UploadedArchive.def()
    }
}

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
            self.created_at = Set(Some(now.naive_utc()));
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(Some(now.naive_utc()));
        }
        Ok(self)
    }
}
