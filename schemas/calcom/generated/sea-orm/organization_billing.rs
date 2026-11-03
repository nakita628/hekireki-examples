use super::billing_mode::BillingMode;
use super::billing_period::BillingPeriod;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "OrganizationBilling")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "teamId")]
    pub team_id: i32,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "subscriptionId")]
    pub subscription_id: String,
    #[sea_orm(column_name = "subscriptionItemId")]
    pub subscription_item_id: String,
    #[sea_orm(column_name = "customerId")]
    pub customer_id: String,
    pub status: String,
    #[sea_orm(column_name = "planName")]
    pub plan_name: String,
    #[sea_orm(column_name = "subscriptionStart")]
    pub subscription_start: Option<DateTime>,
    #[sea_orm(column_name = "subscriptionTrialEnd")]
    pub subscription_trial_end: Option<DateTime>,
    #[sea_orm(column_name = "subscriptionEnd")]
    pub subscription_end: Option<DateTime>,
    #[sea_orm(column_name = "billingPeriod")]
    pub billing_period: Option<BillingPeriod>,
    #[sea_orm(column_name = "billingMode", default_value = "SEATS")]
    pub billing_mode: BillingMode,
    #[sea_orm(column_name = "pricePerSeat")]
    pub price_per_seat: Option<i32>,
    #[sea_orm(column_name = "paidSeats")]
    pub paid_seats: Option<i32>,
    #[sea_orm(column_name = "highWaterMark")]
    pub high_water_mark: Option<i32>,
    #[sea_orm(column_name = "highWaterMarkPeriodStart")]
    pub high_water_mark_period_start: Option<DateTime>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::team::Entity",
        from = "Column::TeamId",
        to = "super::team::Column::Id",
        on_delete = "Cascade"
    )]
    Team,
    #[sea_orm(has_many = "super::monthly_proration::Entity")]
    MonthlyProrations,
    #[sea_orm(has_many = "super::seat_change_log::Entity")]
    SeatChangeLogs,
}

impl Related<super::team::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Team.def()
    }
}

impl Related<super::monthly_proration::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::MonthlyProrations.def()
    }
}

impl Related<super::seat_change_log::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SeatChangeLogs.def()
    }
}

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
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.naive_utc());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
