use super::plan_types::PlanTypes;
use super::subscription_status::SubscriptionStatus;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Subscription")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    pub plan: PlanTypes,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "stripeSubscriptionId")]
    pub stripe_subscription_id: Option<String>,
    pub status: SubscriptionStatus,
    #[sea_orm(column_name = "startDate")]
    pub start_date: Option<DateTime>,
    #[sea_orm(column_name = "endDate")]
    pub end_date: Option<DateTime>,
    #[sea_orm(column_name = "lastPaymentFailedDate")]
    pub last_payment_failed_date: Option<DateTime>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "maxMembers")]
    pub max_members: Option<i32>,
    #[sea_orm(column_name = "maxProjects")]
    pub max_projects: Option<i32>,
    #[sea_orm(column_name = "maxMessagesPerMonth")]
    pub max_messages_per_month: Option<i32>,
    #[sea_orm(column_name = "maxWorkflows")]
    pub max_workflows: Option<i32>,
    #[sea_orm(column_name = "maxRetentionDays")]
    pub max_retention_days: Option<i32>,
    #[sea_orm(column_name = "maxMembersLite")]
    pub max_members_lite: Option<i32>,
    #[sea_orm(column_name = "maxMembersDeveloper")]
    pub max_members_developer: Option<i32>,
    #[sea_orm(column_name = "maxTeams")]
    pub max_teams: Option<i32>,
    #[sea_orm(column_name = "maxPrompts")]
    pub max_prompts: Option<i32>,
    #[sea_orm(column_name = "maxEvaluators")]
    pub max_evaluators: Option<i32>,
    #[sea_orm(column_name = "maxScenarios")]
    pub max_scenarios: Option<i32>,
    #[sea_orm(column_name = "maxAgents")]
    pub max_agents: Option<i32>,
    #[sea_orm(column_name = "maxExperiments")]
    pub max_experiments: Option<i32>,
    #[sea_orm(column_name = "maxOnlineEvaluations")]
    pub max_online_evaluations: Option<i32>,
    #[sea_orm(column_name = "maxDatasets")]
    pub max_datasets: Option<i32>,
    #[sea_orm(column_name = "maxDashboards")]
    pub max_dashboards: Option<i32>,
    #[sea_orm(column_name = "maxCustomGraphs")]
    pub max_custom_graphs: Option<i32>,
    #[sea_orm(column_name = "maxAutomations")]
    pub max_automations: Option<i32>,
    #[sea_orm(column_name = "seatsKept")]
    pub seats_kept: Option<i32>,
    #[sea_orm(column_name = "seatsKeptUntil")]
    pub seats_kept_until: Option<DateTime>,
    #[sea_orm(column_name = "seatLineQuantity")]
    pub seat_line_quantity: Option<i32>,
    #[sea_orm(column_name = "seatLineQuantityAt")]
    pub seat_line_quantity_at: Option<DateTime>,
    #[sea_orm(column_name = "seatLineWriteQuantities")]
    pub seat_line_write_quantities: Vec<i32>,
    #[sea_orm(column_name = "seatLineWriteSince")]
    pub seat_line_write_since: Option<DateTime>,
    #[sea_orm(column_name = "seatLineWriteAt")]
    pub seat_line_write_at: Option<DateTime>,
    #[sea_orm(column_name = "seatLineWriteAttempts")]
    pub seat_line_write_attempts: Option<i32>,
    #[sea_orm(column_name = "seatLineWriteFlaggedAt")]
    pub seat_line_write_flagged_at: Option<DateTime>,
    #[sea_orm(column_name = "seatRepairQuantity")]
    pub seat_repair_quantity: Option<i32>,
    #[sea_orm(column_name = "seatRepairLiftedQuantity")]
    pub seat_repair_lifted_quantity: Option<i32>,
    #[sea_orm(column_name = "seatRepairSince")]
    pub seat_repair_since: Option<DateTime>,
    #[sea_orm(column_name = "seatRepairAttempts")]
    pub seat_repair_attempts: Option<i32>,
    #[sea_orm(column_name = "seatRepairFlaggedAt")]
    pub seat_repair_flagged_at: Option<DateTime>,
    #[sea_orm(column_name = "seatLineLeaseToken")]
    pub seat_line_lease_token: Option<String>,
    #[sea_orm(column_name = "seatLineLeaseUntil")]
    pub seat_line_lease_until: Option<DateTime>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::organization::Entity",
        from = "Column::OrganizationId",
        to = "super::organization::Column::Id"
    )]
    Organization,
    #[sea_orm(has_many = "super::invoice::Entity")]
    Invoices,
}

impl Related<super::organization::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organization.def()
    }
}

impl Related<super::invoice::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Invoices.def()
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
            self.created_at = Set(now.naive_utc());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
