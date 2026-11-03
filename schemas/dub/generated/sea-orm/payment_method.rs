use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "PaymentMethod")]
pub enum PaymentMethod {
    #[sea_orm(string_value = "card")]
    Card,
    #[sea_orm(string_value = "ach")]
    Ach,
    #[sea_orm(string_value = "ach_fast")]
    AchFast,
    #[sea_orm(string_value = "sepa")]
    Sepa,
    #[sea_orm(string_value = "acss")]
    Acss,
}
