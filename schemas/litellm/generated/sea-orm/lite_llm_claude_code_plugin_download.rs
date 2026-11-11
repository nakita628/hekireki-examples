use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_ClaudeCodePluginDownload")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i64,
    pub plugin_id: String,
    pub downloaded_at: DateTime,
    pub version: Option<String>,
    pub digest: String,
    pub authenticated: bool,
    pub user_id: Option<String>,
    pub user_email: Option<String>,
    pub key_alias: Option<String>,
    pub team_id: Option<String>,
    pub team_alias: Option<String>,
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

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.downloaded_at.is_not_set() {
            self.downloaded_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
