use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "ProjectDisabledReason"
)]
pub enum ProjectDisabledReason {
    #[sea_orm(string_value = "PAYMENT_FAILED")]
    PaymentFailed,
    #[sea_orm(string_value = "EMAIL_REPUTATION")]
    EmailReputation,
    #[sea_orm(string_value = "PHISHING_DETECTED")]
    PhishingDetected,
    #[sea_orm(string_value = "CARD_VERIFICATION_FAILED")]
    CardVerificationFailed,
    #[sea_orm(string_value = "CARD_VERIFICATION_REQUIRED")]
    CardVerificationRequired,
    #[sea_orm(string_value = "MANUAL")]
    Manual,
}
