use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_ClaudeCodePluginVersion")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub plugin_id: String,
    pub version: Option<String>,
    pub description: Option<String>,
    pub manifest_json: Option<String>,
    pub published_at: Option<DateTime>,
    pub published_by: Option<String>,
    #[sea_orm(default_value = false)]
    pub archive_available: bool,
    pub content: Option<Vec<u8>>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::lite_llm_claude_code_plugin_table::Entity",
        from = "Column::PluginId",
        to = "super::lite_llm_claude_code_plugin_table::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    Plugin,
}

impl Related<super::lite_llm_claude_code_plugin_table::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Plugin.def()
    }
}

impl ActiveModelBehavior for ActiveModel {
    fn new() -> Self {
        Self {
            id: Set(uuid::Uuid::new_v4().to_string()),
            ..ActiveModelTrait::default()
        }
    }
}
