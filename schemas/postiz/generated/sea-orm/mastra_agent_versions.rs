use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "mastra_agent_versions")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "agentId")]
    pub agent_id: String,
    #[sea_orm(column_name = "versionNumber")]
    pub version_number: i32,
    pub name: String,
    pub description: Option<String>,
    pub instructions: String,
    pub model: Json,
    pub tools: Option<Json>,
    #[sea_orm(column_name = "defaultOptions")]
    pub default_options: Option<Json>,
    pub workflows: Option<Json>,
    pub agents: Option<Json>,
    #[sea_orm(column_name = "integrationTools")]
    pub integration_tools: Option<Json>,
    #[sea_orm(column_name = "inputProcessors")]
    pub input_processors: Option<Json>,
    #[sea_orm(column_name = "outputProcessors")]
    pub output_processors: Option<Json>,
    pub memory: Option<Json>,
    pub scorers: Option<Json>,
    #[sea_orm(column_name = "mcpClients")]
    pub mcp_clients: Option<Json>,
    #[sea_orm(column_name = "requestContextSchema")]
    pub request_context_schema: Option<Json>,
    pub workspace: Option<Json>,
    pub skills: Option<Json>,
    #[sea_orm(column_name = "skillsFormat")]
    pub skills_format: Option<String>,
    #[sea_orm(column_name = "changedFields")]
    pub changed_fields: Option<Json>,
    #[sea_orm(column_name = "changeMessage")]
    pub change_message: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "createdAtZ", column_type = "TimestampWithTimeZone")]
    pub created_at_z: Option<DateTimeWithTimeZone>,
    pub durable: Option<Json>,
    pub browser: Option<Json>,
    #[sea_orm(column_name = "toolProviders")]
    pub tool_providers: Option<Json>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_at_z.is_not_set() {
            self.created_at_z = Set(Some(now.fixed_offset()));
        }
        Ok(self)
    }
}
