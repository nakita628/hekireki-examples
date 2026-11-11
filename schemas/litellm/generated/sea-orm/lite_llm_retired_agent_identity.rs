use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_RetiredAgentIdentity")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub binding_id: String,
    pub agent_id: Option<String>,
    pub provider: String,
    pub issuer: String,
    pub tenant_id: String,
    pub client_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::lite_llm_agents_table::Entity",
        from = "Column::AgentId",
        to = "super::lite_llm_agents_table::Column::AgentId",
        on_delete = "SetNull"
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
            binding_id: Set(uuid::Uuid::new_v4().to_string()),
            ..ActiveModelTrait::default()
        }
    }
}
