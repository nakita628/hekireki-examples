use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_VerificationToken")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub token: String,
    pub key_name: Option<String>,
    pub key_alias: Option<String>,
    #[sea_orm(default_value = false)]
    pub soft_budget_cooldown: bool,
    #[sea_orm(default_value = 0)]
    pub spend: f64,
    #[sea_orm(default_value = 0)]
    pub total_spend: f64,
    pub expires: Option<DateTime>,
    pub models: Vec<String>,
    #[sea_orm(default_value = "{}")]
    pub aliases: Json,
    #[sea_orm(default_value = "{}")]
    pub config: Json,
    #[sea_orm(default_value = "{}")]
    pub router_settings: Option<Json>,
    pub user_id: Option<String>,
    pub team_id: Option<String>,
    pub agent_id: Option<String>,
    pub project_id: Option<String>,
    #[sea_orm(default_value = "{}")]
    pub permissions: Json,
    pub max_parallel_requests: Option<i32>,
    #[sea_orm(default_value = "{}")]
    pub metadata: Json,
    pub blocked: Option<bool>,
    pub tpm_limit: Option<i64>,
    pub rpm_limit: Option<i64>,
    pub tpd_limit: Option<i64>,
    pub max_budget: Option<f64>,
    pub budget_duration: Option<String>,
    pub budget_reset_at: Option<DateTime>,
    pub allowed_cache_controls: Vec<String>,
    pub allowed_routes: Vec<String>,
    pub key_type: Option<String>,
    pub policies: Vec<String>,
    pub access_group_ids: Vec<String>,
    #[sea_orm(default_value = "{}")]
    pub model_spend: Json,
    #[sea_orm(default_value = "{}")]
    pub model_max_budget: Json,
    #[sea_orm(default_value = "{}")]
    pub budget_fallbacks: Json,
    pub budget_id: Option<String>,
    pub organization_id: Option<String>,
    pub object_permission_id: Option<String>,
    pub created_at: Option<DateTime>,
    pub created_by: Option<String>,
    pub updated_at: Option<DateTime>,
    pub updated_by: Option<String>,
    pub settings_updated_at: Option<DateTime>,
    pub last_active: Option<DateTime>,
    #[sea_orm(default_value = 0)]
    pub rotation_count: Option<i32>,
    #[sea_orm(default_value = false)]
    pub auto_rotate: Option<bool>,
    pub rotation_interval: Option<String>,
    pub last_rotation_at: Option<DateTime>,
    pub key_rotation_at: Option<DateTime>,
    pub budget_limits: Option<Json>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::lite_llm_budget_table::Entity",
        from = "Column::BudgetId",
        to = "super::lite_llm_budget_table::Column::BudgetId"
    )]
    LitellmBudgetTable,
    #[sea_orm(
        belongs_to = "super::lite_llm_organization_table::Entity",
        from = "Column::OrganizationId",
        to = "super::lite_llm_organization_table::Column::OrganizationId"
    )]
    LitellmOrganizationTable,
    #[sea_orm(
        belongs_to = "super::lite_llm_project_table::Entity",
        from = "Column::ProjectId",
        to = "super::lite_llm_project_table::Column::ProjectId"
    )]
    LitellmProjectTable,
    #[sea_orm(
        belongs_to = "super::lite_llm_object_permission_table::Entity",
        from = "Column::ObjectPermissionId",
        to = "super::lite_llm_object_permission_table::Column::ObjectPermissionId"
    )]
    ObjectPermission,
    #[sea_orm(has_many = "super::lite_llm_jwtkey_mapping::Entity")]
    JwtKeyMappings,
}

impl Related<super::lite_llm_budget_table::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::LitellmBudgetTable.def()
    }
}

impl Related<super::lite_llm_organization_table::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::LitellmOrganizationTable.def()
    }
}

impl Related<super::lite_llm_project_table::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::LitellmProjectTable.def()
    }
}

impl Related<super::lite_llm_object_permission_table::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ObjectPermission.def()
    }
}

impl Related<super::lite_llm_jwtkey_mapping::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::JwtKeyMappings.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
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
