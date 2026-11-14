use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LicenseRenewalClaim")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "stripeInvoiceId")]
    pub stripe_invoice_id: String,
    #[sea_orm(column_name = "stripeSubscriptionId")]
    pub stripe_subscription_id: String,
    #[sea_orm(column_name = "claimToken")]
    pub claim_token: String,
    #[sea_orm(column_name = "claimedAt")]
    pub claimed_at: DateTime,
    #[sea_orm(column_name = "issuedLicense")]
    pub issued_license: Option<Json>,
    #[sea_orm(column_name = "deliveredAt")]
    pub delivered_at: Option<DateTime>,
    #[sea_orm(column_name = "licenseId")]
    pub license_id: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

impl ActiveModelBehavior for ActiveModel {}
