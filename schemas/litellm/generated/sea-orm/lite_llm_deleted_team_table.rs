use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_DeletedTeamTable")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
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
    #[sea_orm(default_value = "{}")]
    pub model_spend: Json,
    #[sea_orm(default_value = "{}")]
    pub model_max_budget: Json,
    #[sea_orm(default_value = "{}")]
    pub router_settings: Option<Json>,
    pub team_member_permissions: Vec<String>,
    pub access_group_ids: Vec<String>,
    pub policies: Vec<String>,
    pub model_id: Option<i32>,
    #[sea_orm(default_value = false)]
    pub allow_team_guardrail_config: bool,
    pub created_at: Option<DateTime>,
    pub updated_at: Option<DateTime>,
    pub deleted_at: DateTime,
    pub deleted_by: Option<String>,
    pub deleted_by_api_key: Option<String>,
    pub litellm_changed_by: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    fn new() -> Self {
        Self {
            id: Set(uuid::Uuid::new_v4().to_string()),
            ..ActiveModelTrait::default()
        }
    }

    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.deleted_at.is_not_set() {
            self.deleted_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
