use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_InvitationLink")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub user_id: String,
    #[sea_orm(default_value = false)]
    pub is_accepted: bool,
    pub accepted_at: Option<DateTime>,
    pub expires_at: DateTime,
    pub created_at: DateTime,
    pub created_by: String,
    pub updated_at: DateTime,
    pub updated_by: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::lite_llm_user_table::Entity",
        from = "Column::UserId",
        to = "super::lite_llm_user_table::Column::UserId"
    )]
    LiteLLMUserTableUser,
    #[sea_orm(
        belongs_to = "super::lite_llm_user_table::Entity",
        from = "Column::CreatedBy",
        to = "super::lite_llm_user_table::Column::UserId"
    )]
    LiteLLMUserTableCreated,
    #[sea_orm(
        belongs_to = "super::lite_llm_user_table::Entity",
        from = "Column::UpdatedBy",
        to = "super::lite_llm_user_table::Column::UserId"
    )]
    LiteLLMUserTableUpdated,
}

impl Related<super::lite_llm_user_table::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::LiteLLMUserTableUser.def()
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
