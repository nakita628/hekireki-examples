use super::premium_tier::PremiumTier;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Premium")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "pendingInvites")]
    pub pending_invites: Vec<String>,
    #[sea_orm(column_name = "lemonSqueezyRenewsAt")]
    pub lemon_squeezy_renews_at: Option<DateTime>,
    #[sea_orm(column_name = "lemonSqueezyCustomerId")]
    pub lemon_squeezy_customer_id: Option<i32>,
    #[sea_orm(column_name = "lemonSqueezySubscriptionId")]
    pub lemon_squeezy_subscription_id: Option<i32>,
    #[sea_orm(column_name = "lemonSqueezySubscriptionItemId")]
    pub lemon_squeezy_subscription_item_id: Option<i32>,
    #[sea_orm(column_name = "lemonSqueezyOrderId")]
    pub lemon_squeezy_order_id: Option<i32>,
    #[sea_orm(column_name = "lemonSqueezyProductId")]
    pub lemon_squeezy_product_id: Option<i32>,
    #[sea_orm(column_name = "lemonSqueezyVariantId")]
    pub lemon_squeezy_variant_id: Option<i32>,
    #[sea_orm(column_name = "lemonLicenseKey")]
    pub lemon_license_key: Option<String>,
    #[sea_orm(column_name = "lemonLicenseInstanceId")]
    pub lemon_license_instance_id: Option<String>,
    #[sea_orm(column_name = "lemonSubscriptionStatus")]
    pub lemon_subscription_status: Option<String>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "stripeCustomerId")]
    pub stripe_customer_id: Option<String>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "stripeSubscriptionId")]
    pub stripe_subscription_id: Option<String>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "stripeSubscriptionItemId")]
    pub stripe_subscription_item_id: Option<String>,
    #[sea_orm(column_name = "stripePriceId")]
    pub stripe_price_id: Option<String>,
    #[sea_orm(column_name = "stripeProductId")]
    pub stripe_product_id: Option<String>,
    #[sea_orm(column_name = "stripeSubscriptionStatus")]
    pub stripe_subscription_status: Option<String>,
    #[sea_orm(column_name = "stripeCancelAt")]
    pub stripe_cancel_at: Option<DateTime>,
    #[sea_orm(column_name = "stripeCancelAtPeriodEnd")]
    pub stripe_cancel_at_period_end: Option<bool>,
    #[sea_orm(column_name = "stripeRenewsAt")]
    pub stripe_renews_at: Option<DateTime>,
    #[sea_orm(column_name = "stripeTrialEnd")]
    pub stripe_trial_end: Option<DateTime>,
    #[sea_orm(column_name = "stripeTrialConversionInvoiceId")]
    pub stripe_trial_conversion_invoice_id: Option<String>,
    #[sea_orm(column_name = "stripeTrialConvertedAt")]
    pub stripe_trial_converted_at: Option<DateTime>,
    #[sea_orm(column_name = "stripeCancellationInitiatedAt")]
    pub stripe_cancellation_initiated_at: Option<DateTime>,
    #[sea_orm(column_name = "stripeCanceledAt")]
    pub stripe_canceled_at: Option<DateTime>,
    #[sea_orm(column_name = "stripeEndedAt")]
    pub stripe_ended_at: Option<DateTime>,
    #[sea_orm(column_name = "stripeCancellationFeedback")]
    pub stripe_cancellation_feedback: Option<String>,
    #[sea_orm(column_name = "stripeCancellationComment")]
    pub stripe_cancellation_comment: Option<String>,
    #[sea_orm(column_name = "stripeAiOverageLastInvoiceId")]
    pub stripe_ai_overage_last_invoice_id: Option<String>,
    #[sea_orm(column_name = "stripeAiOverageLastPeriodEnd")]
    pub stripe_ai_overage_last_period_end: Option<DateTime>,
    #[sea_orm(column_name = "stripeAiOverageLastUnits")]
    pub stripe_ai_overage_last_units: Option<i32>,
    #[sea_orm(column_name = "stripeInvoiceEmailsEnabled", default_value = false)]
    pub stripe_invoice_emails_enabled: bool,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "appleOriginalTransactionId")]
    pub apple_original_transaction_id: Option<String>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "appleLatestTransactionId")]
    pub apple_latest_transaction_id: Option<String>,
    #[sea_orm(column_name = "appleProductId")]
    pub apple_product_id: Option<String>,
    #[sea_orm(column_name = "appleSubscriptionGroupIdentifier")]
    pub apple_subscription_group_identifier: Option<String>,
    #[sea_orm(column_name = "appleAppAccountToken")]
    pub apple_app_account_token: Option<String>,
    #[sea_orm(column_name = "appleSubscriptionStatus")]
    pub apple_subscription_status: Option<String>,
    #[sea_orm(column_name = "appleEnvironment")]
    pub apple_environment: Option<String>,
    #[sea_orm(column_name = "applePurchaseDate")]
    pub apple_purchase_date: Option<DateTime>,
    #[sea_orm(column_name = "appleExpiresAt")]
    pub apple_expires_at: Option<DateTime>,
    #[sea_orm(column_name = "appleRevokedAt")]
    pub apple_revoked_at: Option<DateTime>,
    #[sea_orm(column_name = "appleOfferDiscountType")]
    pub apple_offer_discount_type: Option<String>,
    #[sea_orm(column_name = "adminGrantExpiresAt")]
    pub admin_grant_expires_at: Option<DateTime>,
    #[sea_orm(column_name = "adminGrantTier")]
    pub admin_grant_tier: Option<PremiumTier>,
    pub tier: Option<PremiumTier>,
    #[sea_orm(column_name = "emailAccountsAccess")]
    pub email_accounts_access: Option<i32>,
    #[sea_orm(column_name = "unsubscribeMonth")]
    pub unsubscribe_month: Option<i32>,
    #[sea_orm(column_name = "unsubscribeCredits")]
    pub unsubscribe_credits: Option<i32>,
    #[sea_orm(column_name = "aiMonth")]
    pub ai_month: Option<i32>,
    #[sea_orm(column_name = "aiCredits")]
    pub ai_credits: Option<i32>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        has_many = "super::user::Entity",
        from = "Column::Id",
        to = "super::user::Column::PremiumId"
    )]
    Users,
    #[sea_orm(
        has_many = "super::user::Entity",
        from = "Column::Id",
        to = "super::user::Column::PremiumAdminId"
    )]
    Admins,
    #[sea_orm(has_many = "super::payment::Entity")]
    Payments,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Users.def()
    }
}

impl Related<super::payment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Payments.def()
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
