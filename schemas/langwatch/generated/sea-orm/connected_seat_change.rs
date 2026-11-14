use super::connected_seat_change_state::ConnectedSeatChangeState;
use super::currency::Currency;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "ConnectedSeatChange")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "licenseId")]
    pub license_id: String,
    #[sea_orm(column_name = "accountId")]
    pub account_id: Option<String>,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: Option<String>,
    #[sea_orm(column_name = "changedAt")]
    pub changed_at: DateTime,
    #[sea_orm(column_name = "addedSeats")]
    pub added_seats: i32,
    #[sea_orm(column_name = "unitAmountCents")]
    pub unit_amount_cents: i32,
    #[sea_orm(column_name = "amountCents")]
    pub amount_cents: i32,
    pub currency: Option<Currency>,
    #[sea_orm(column_name = "stripeInvoiceId")]
    pub stripe_invoice_id: Option<String>,
    pub state: ConnectedSeatChangeState,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

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
