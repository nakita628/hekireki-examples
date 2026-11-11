use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_TeamTable")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub team_id: String,
    pub team_alias: Option<String>,
    pub organization_id: Option<String>,
    pub object_permission_id: Option<String>,
    pub admins: Vec<String>,
    pub members: Vec<String>,
    #[sea_orm(default_value = "{}")]
    pub members_with_roles: Json,
    #[sea_orm(default_value = "{}")]
    pub metadata: Json,
    pub max_budget: Option<f64>,
    pub soft_budget: Option<f64>,
    #[sea_orm(default_value = 0)]
    pub spend: f64,
    pub models: Vec<String>,
    pub max_parallel_requests: Option<i32>,
    pub tpm_limit: Option<i64>,
    pub rpm_limit: Option<i64>,
    pub tpd_limit: Option<i64>,
    pub budget_duration: Option<String>,
    pub budget_reset_at: Option<DateTime>,
    #[sea_orm(default_value = false)]
    pub blocked: bool,
    pub created_at: DateTime,
    pub updated_at: DateTime,
    #[sea_orm(default_value = "{}")]
    pub model_spend: Json,
    #[sea_orm(default_value = "{}")]
    pub model_max_budget: Json,
    #[sea_orm(default_value = "{}")]
    pub router_settings: Option<Json>,
    pub team_member_permissions: Vec<String>,
    pub access_group_ids: Vec<String>,
    pub policies: Vec<String>,
    pub default_team_member_models: Vec<String>,
    pub budget_limits: Option<Json>,
    #[sea_orm(unique)]
    pub model_id: Option<i32>,
    #[sea_orm(default_value = false)]
    pub allow_team_guardrail_config: bool,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::lite_llm_organization_table::Entity",
        from = "Column::OrganizationId",
        to = "super::lite_llm_organization_table::Column::OrganizationId"
    )]
    LitellmOrganizationTable,
    #[sea_orm(
        belongs_to = "super::lite_llm_model_table::Entity",
        from = "Column::ModelId",
        to = "super::lite_llm_model_table::Column::Id"
    )]
    LitellmModelTable,
    #[sea_orm(
        belongs_to = "super::lite_llm_object_permission_table::Entity",
        from = "Column::ObjectPermissionId",
        to = "super::lite_llm_object_permission_table::Column::ObjectPermissionId"
    )]
    ObjectPermission,
    #[sea_orm(has_many = "super::lite_llm_project_table::Entity")]
    Projects,
}

impl Related<super::lite_llm_organization_table::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::LitellmOrganizationTable.def()
    }
}

impl Related<super::lite_llm_model_table::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::LitellmModelTable.def()
    }
}

impl Related<super::lite_llm_object_permission_table::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ObjectPermission.def()
    }
}

impl Related<super::lite_llm_project_table::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Projects.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    fn new() -> Self {
        Self {
            team_id: Set(uuid::Uuid::new_v4().to_string()),
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
