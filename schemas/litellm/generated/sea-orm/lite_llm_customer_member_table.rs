use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_CustomerMemberTable")]
pub struct Model {
    #[sea_orm(default_value = 0)]
    pub spend: f64,
    #[sea_orm(primary_key, auto_increment = false)]
    pub customer_member_id: String,
    pub alias: Option<String>,
    pub parent_org_id: String,
    pub parent_team_ids: Vec<String>,
    pub budget_id: Option<String>,
    pub budget_override_id: Option<String>,
    pub budget_override_expires_at: Option<DateTime>,
    pub created_at: DateTime,
    pub created_by: String,
    pub updated_at: DateTime,
    pub updated_by: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::lite_llm_customer_organization_table::Entity",
        from = "Column::ParentOrgId",
        to = "super::lite_llm_customer_organization_table::Column::CustomerOrgId"
    )]
    ParentOrg,
    #[sea_orm(
        belongs_to = "super::lite_llm_budget_table::Entity",
        from = "Column::BudgetId",
        to = "super::lite_llm_budget_table::Column::BudgetId",
        on_delete = "Restrict"
    )]
    Budget,
    #[sea_orm(
        belongs_to = "super::lite_llm_budget_table::Entity",
        from = "Column::BudgetOverrideId",
        to = "super::lite_llm_budget_table::Column::BudgetId",
        on_delete = "Restrict"
    )]
    BudgetOverride,
    #[sea_orm(has_many = "super::lite_llm_customer_budget_increase_request::Entity")]
    BudgetIncreaseRequests,
}

impl Related<super::lite_llm_customer_organization_table::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ParentOrg.def()
    }
}

impl Related<super::lite_llm_budget_table::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Budget.def()
    }
}

impl Related<super::lite_llm_customer_budget_increase_request::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BudgetIncreaseRequests.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    fn new() -> Self {
        Self {
            customer_member_id: Set(uuid::Uuid::new_v4().to_string()),
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
