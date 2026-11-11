use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_AgentIdentity")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub agent_id: String,
    #[sea_orm(default_value = true)]
    pub active: bool,
    pub provider: String,
    pub issuer: String,
    pub tenant_id: String,
    pub client_id: String,
    pub service_principal_id: Option<String>,
    pub required_roles: Vec<String>,
    pub required_scopes: Vec<String>,
    pub revision: String,
    pub last_authenticated_at: Option<DateTime>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::lite_llm_agents_table::Entity",
        from = "Column::AgentId",
        to = "super::lite_llm_agents_table::Column::AgentId",
        on_delete = "Cascade"
    )]
    Agent,
}

impl Related<super::lite_llm_agents_table::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Agent.def()
    }
}

impl ActiveModelBehavior for ActiveModel {
    fn new() -> Self {
        Self {
            revision: Set(uuid::Uuid::new_v4().to_string()),
            ..ActiveModelTrait::default()
        }
    }
}
