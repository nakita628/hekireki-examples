use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_BudgetTable")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub budget_id: String,
    pub name: Option<String>,
    pub max_budget: Option<f64>,
    pub soft_budget: Option<f64>,
    pub max_parallel_requests: Option<i32>,
    pub tpm_limit: Option<i64>,
    pub rpm_limit: Option<i64>,
    pub tpd_limit: Option<i64>,
    pub model_max_budget: Option<Json>,
    pub model_tpm_limit: Option<Json>,
    pub model_rpm_limit: Option<Json>,
    pub budget_duration: Option<String>,
    pub budget_limits: Option<Json>,
    pub cap_policy: Option<String>,
    pub budget_reset_at: Option<DateTime>,
    pub allowed_models: Vec<String>,
    pub temp_budget_increase: Option<f64>,
    pub temp_budget_expiry: Option<DateTime>,
    pub created_at: DateTime,
    pub created_by: String,
    pub updated_at: DateTime,
    pub updated_by: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        has_many = "super::lite_llm_customer_organization_table::Entity",
        from = "Column::BudgetId",
        to = "super::lite_llm_customer_organization_table::Column::DefaultBudgetTemplateId"
    )]
    CustomerOrganizationDefaults,
    #[sea_orm(
        has_many = "super::lite_llm_customer_organization_table::Entity",
        from = "Column::BudgetId",
        to = "super::lite_llm_customer_organization_table::Column::BudgetId"
    )]
    CustomerOrganizations,
    #[sea_orm(
        has_many = "super::lite_llm_customer_team_table::Entity",
        from = "Column::BudgetId",
        to = "super::lite_llm_customer_team_table::Column::BudgetId"
    )]
    CustomerTeams,
    #[sea_orm(
        has_many = "super::lite_llm_customer_member_table::Entity",
        from = "Column::BudgetId",
        to = "super::lite_llm_customer_member_table::Column::BudgetId"
    )]
    CustomerMembers,
    #[sea_orm(
        has_many = "super::lite_llm_customer_member_table::Entity",
        from = "Column::BudgetId",
        to = "super::lite_llm_customer_member_table::Column::BudgetOverrideId"
    )]
    CustomerMemberOverrides,
    #[sea_orm(
        has_many = "super::lite_llm_customer_team_table::Entity",
        from = "Column::BudgetId",
        to = "super::lite_llm_customer_team_table::Column::BudgetOverrideId"
    )]
    CustomerTeamOverrides,
    #[sea_orm(has_many = "super::lite_llm_organization_table::Entity")]
    Organization,
    #[sea_orm(has_many = "super::lite_llm_project_table::Entity")]
    Projects,
    #[sea_orm(has_many = "super::lite_llm_verification_token::Entity")]
    Keys,
    #[sea_orm(has_many = "super::lite_llm_end_user_table::Entity")]
    EndUsers,
    #[sea_orm(has_many = "super::lite_llm_tag_table::Entity")]
    Tags,
    #[sea_orm(has_many = "super::lite_llm_model_access_group_budget_table::Entity")]
    ModelAccessGroups,
    #[sea_orm(has_many = "super::lite_llm_team_membership::Entity")]
    TeamMembership,
    #[sea_orm(has_many = "super::lite_llm_organization_membership::Entity")]
    OrganizationMembership,
}

impl Related<super::lite_llm_customer_organization_table::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CustomerOrganizationDefaults.def()
    }
}

impl Related<super::lite_llm_customer_team_table::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CustomerTeams.def()
    }
}

impl Related<super::lite_llm_customer_member_table::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CustomerMembers.def()
    }
}

impl Related<super::lite_llm_organization_table::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organization.def()
    }
}

impl Related<super::lite_llm_project_table::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Projects.def()
    }
}

impl Related<super::lite_llm_verification_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Keys.def()
    }
}

impl Related<super::lite_llm_end_user_table::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EndUsers.def()
    }
}

impl Related<super::lite_llm_tag_table::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Tags.def()
    }
}

impl Related<super::lite_llm_model_access_group_budget_table::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ModelAccessGroups.def()
    }
}

impl Related<super::lite_llm_team_membership::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TeamMembership.def()
    }
}

impl Related<super::lite_llm_organization_membership::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OrganizationMembership.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    fn new() -> Self {
        Self {
            budget_id: Set(uuid::Uuid::new_v4().to_string()),
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
