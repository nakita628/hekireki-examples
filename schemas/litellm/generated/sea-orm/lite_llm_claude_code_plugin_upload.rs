use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_ClaudeCodePluginUpload")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub plugin_name: String,
    pub content: Vec<u8>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::lite_llm_claude_code_plugin_table::Entity",
        from = "Column::PluginName",
        to = "super::lite_llm_claude_code_plugin_table::Column::Name",
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

impl ActiveModelBehavior for ActiveModel {}
