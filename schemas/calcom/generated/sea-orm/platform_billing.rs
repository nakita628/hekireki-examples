use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "PlatformBilling")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: i32,
    #[sea_orm(column_name = "customerId")]
    pub customer_id: String,
    #[sea_orm(column_name = "subscriptionId")]
    pub subscription_id: Option<String>,
    #[sea_orm(column_name = "priceId")]
    pub price_id: Option<String>,
    #[sea_orm(default_value = "none")]
    pub plan: String,
    #[sea_orm(column_name = "billingCycleStart")]
    pub billing_cycle_start: Option<i32>,
    #[sea_orm(column_name = "billingCycleEnd")]
    pub billing_cycle_end: Option<i32>,
    #[sea_orm(default_value = false)]
    pub overdue: Option<bool>,
    #[sea_orm(column_name = "managerBillingId")]
    pub manager_billing_id: Option<i32>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::platform_billing::Entity",
        from = "Column::ManagerBillingId",
        to = "super::platform_billing::Column::Id"
    )]
    ManagerBilling,
    #[sea_orm(
        belongs_to = "super::team::Entity",
        from = "Column::Id",
        to = "super::team::Column::Id",
        on_delete = "Cascade"
    )]
    Team,
    #[sea_orm(
        has_many = "super::platform_billing::Entity",
        from = "Column::Id",
        to = "super::platform_billing::Column::ManagerBillingId"
    )]
    ManagedBillings,
}

impl Related<super::platform_billing::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ManagerBilling.def()
    }
}

impl Related<super::team::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Team.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
