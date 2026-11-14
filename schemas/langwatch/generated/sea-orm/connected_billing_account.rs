use super::currency::Currency;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "ConnectedBillingAccount")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "stripeCustomerId")]
    pub stripe_customer_id: String,
    #[sea_orm(column_name = "usageSubscriptionId")]
    pub usage_subscription_id: Option<String>,
    #[sea_orm(column_name = "usageSubscriptionItemId")]
    pub usage_subscription_item_id: Option<String>,
    #[sea_orm(column_name = "termStartsAt")]
    pub term_starts_at: DateTime,
    #[sea_orm(column_name = "termEndsAt")]
    pub term_ends_at: DateTime,
    #[sea_orm(column_name = "commitUsdCents", default_value = 0)]
    pub commit_usd_cents: i32,
    #[sea_orm(column_name = "seatCurrency")]
    pub seat_currency: Currency,
    #[sea_orm(column_name = "seatRateCents")]
    pub seat_rate_cents: i32,
    pub seats: i32,
    #[sea_orm(column_name = "bankTransferType")]
    pub bank_transfer_type: Option<String>,
    #[sea_orm(column_name = "bankTransferCountry")]
    pub bank_transfer_country: Option<String>,
    #[sea_orm(column_name = "billingEmail")]
    pub billing_email: String,
    #[sea_orm(column_name = "pendingRenewal")]
    pub pending_renewal: Option<Json>,
    #[sea_orm(column_name = "createdById")]
    pub created_by_id: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::organization::Entity",
        from = "Column::OrganizationId",
        to = "super::organization::Column::Id",
        on_delete = "Restrict"
    )]
    Organization,
}

impl Related<super::organization::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organization.def()
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
