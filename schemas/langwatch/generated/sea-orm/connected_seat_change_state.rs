use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "ConnectedSeatChangeState"
)]
pub enum ConnectedSeatChangeState {
    #[sea_orm(string_value = "intent")]
    Intent,
    #[sea_orm(string_value = "invoiced")]
    Invoiced,
    #[sea_orm(string_value = "nothing_to_invoice")]
    NothingToInvoice,
    #[sea_orm(string_value = "not_onboarded")]
    NotOnboarded,
}
