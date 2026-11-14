use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "INVITE_STATUS")]
pub enum INVITE_STATUS {
    #[sea_orm(string_value = "PENDING")]
    Pending,
    #[sea_orm(string_value = "ACCEPTED")]
    Accepted,
    #[sea_orm(string_value = "WAITING_APPROVAL")]
    WaitingApproval,
    #[sea_orm(string_value = "PAYMENT_PENDING")]
    PaymentPending,
    #[sea_orm(string_value = "REVOKED")]
    Revoked,
}
