use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_ProjectTable")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub project_id: String,
    pub project_alias: Option<String>,
    pub description: Option<String>,
    pub team_id: Option<String>,
    pub budget_id: Option<String>,
    #[sea_orm(default_value = "{}")]
    pub metadata: Json,
    pub models: Vec<String>,
    #[sea_orm(default_value = 0)]
    pub spend: f64,
    #[sea_orm(default_value = "{}")]
    pub model_spend: Json,
    #[sea_orm(default_value = "{}")]
    pub model_rpm_limit: Json,
    #[sea_orm(default_value = "{}")]
    pub model_tpm_limit: Json,
    #[sea_orm(default_value = false)]
    pub blocked: bool,
    pub object_permission_id: Option<String>,
    pub created_at: DateTime,
    pub created_by: String,
    pub updated_at: DateTime,
    pub updated_by: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::lite_llm_team_table::Entity",
        from = "Column::TeamId",
        to = "super::lite_llm_team_table::Column::TeamId"
    )]
    LitellmTeamTable,
    #[sea_orm(
        belongs_to = "super::lite_llm_budget_table::Entity",
        from = "Column::BudgetId",
        to = "super::lite_llm_budget_table::Column::BudgetId"
    )]
    LitellmBudgetTable,
    #[sea_orm(
        belongs_to = "super::lite_llm_object_permission_table::Entity",
        from = "Column::ObjectPermissionId",
        to = "super::lite_llm_object_permission_table::Column::ObjectPermissionId"
    )]
    ObjectPermission,
    #[sea_orm(has_many = "super::lite_llm_verification_token::Entity")]
    Keys,
}

impl Related<super::lite_llm_team_table::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::LitellmTeamTable.def()
    }
}

impl Related<super::lite_llm_budget_table::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::LitellmBudgetTable.def()
    }
}

impl Related<super::lite_llm_object_permission_table::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ObjectPermission.def()
    }
}

impl Related<super::lite_llm_verification_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Keys.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    fn new() -> Self {
        Self {
            project_id: Set(uuid::Uuid::new_v4().to_string()),
            ..ActiveModelTrait::default()
        }
    }

    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.naive_utc());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
