use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_ObjectPermissionTable")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub object_permission_id: String,
    pub mcp_servers: Vec<String>,
    pub mcp_access_groups: Vec<String>,
    pub mcp_tool_permissions: Option<Json>,
    pub vector_stores: Vec<String>,
    pub agents: Vec<String>,
    pub agent_access_groups: Vec<String>,
    pub models: Vec<String>,
    pub blocked_tools: Vec<String>,
    pub mcp_toolsets: Vec<String>,
    pub search_tools: Vec<String>,
    pub mcp_tool_search_enabled: Option<bool>,
    pub skills: Vec<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::lite_llm_team_table::Entity")]
    Teams,
    #[sea_orm(has_many = "super::lite_llm_project_table::Entity")]
    Projects,
    #[sea_orm(has_many = "super::lite_llm_verification_token::Entity")]
    VerificationTokens,
    #[sea_orm(has_many = "super::lite_llm_organization_table::Entity")]
    Organizations,
    #[sea_orm(has_many = "super::lite_llm_user_table::Entity")]
    Users,
    #[sea_orm(has_many = "super::lite_llm_end_user_table::Entity")]
    EndUsers,
    #[sea_orm(has_many = "super::lite_llm_agents_table::Entity")]
    AgentsTable,
}

impl Related<super::lite_llm_team_table::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Teams.def()
    }
}

impl Related<super::lite_llm_project_table::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Projects.def()
    }
}

impl Related<super::lite_llm_verification_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::VerificationTokens.def()
    }
}

impl Related<super::lite_llm_organization_table::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organizations.def()
    }
}

impl Related<super::lite_llm_user_table::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Users.def()
    }
}

impl Related<super::lite_llm_end_user_table::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EndUsers.def()
    }
}

impl Related<super::lite_llm_agents_table::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AgentsTable.def()
    }
}

impl ActiveModelBehavior for ActiveModel {
    fn new() -> Self {
        Self {
            object_permission_id: Set(uuid::Uuid::new_v4().to_string()),
            ..ActiveModelTrait::default()
        }
    }
}
