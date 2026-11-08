use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "PaymentProcessor")]
pub enum PaymentProcessor {
    #[sea_orm(string_value = "STRIPE")]
    Stripe,
    #[sea_orm(string_value = "PAYPAL")]
    Paypal,
    #[sea_orm(string_value = "LEMONSQUEEZY")]
    Lemonsqueezy,
    #[sea_orm(string_value = "PADDLE")]
    Paddle,
    #[sea_orm(string_value = "APPLE_PAY")]
    ApplePay,
    #[sea_orm(string_value = "GOOGLE_PAY")]
    GooglePay,
    #[sea_orm(string_value = "REVENUECAT")]
    Revenuecat,
    #[sea_orm(string_value = "NONE")]
    None,
    #[sea_orm(string_value = "OTHER")]
    Other,
}
