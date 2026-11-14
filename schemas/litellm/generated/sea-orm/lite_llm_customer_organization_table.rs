use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_CustomerOrganizationTable")]
pub struct Model {
    #[sea_orm(default_value = 0)]
    pub spend: f64,
    pub budget_template_ids: Vec<String>,
    pub budget_id: Option<String>,
    #[sea_orm(primary_key, auto_increment = false)]
    pub customer_org_id: String,
    pub alias: Option<String>,
    pub default_budget_template_id: Option<String>,
    pub created_at: DateTime,
    pub created_by: String,
    pub updated_at: DateTime,
    pub updated_by: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::lite_llm_budget_table::Entity",
        from = "Column::BudgetId",
        to = "super::lite_llm_budget_table::Column::BudgetId"
    )]
    Budget,
    #[sea_orm(
        belongs_to = "super::lite_llm_budget_table::Entity",
        from = "Column::DefaultBudgetTemplateId",
        to = "super::lite_llm_budget_table::Column::BudgetId"
    )]
    DefaultBudgetTemplate,
    #[sea_orm(has_many = "super::lite_llm_customer_team_table::Entity")]
    Teams,
    #[sea_orm(has_many = "super::lite_llm_customer_member_table::Entity")]
    Members,
}

impl Related<super::lite_llm_budget_table::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Budget.def()
    }
}

impl Related<super::lite_llm_customer_team_table::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Teams.def()
    }
}

impl Related<super::lite_llm_customer_member_table::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Members.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    fn new() -> Self {
        Self {
            customer_org_id: Set(uuid::Uuid::new_v4().to_string()),
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
