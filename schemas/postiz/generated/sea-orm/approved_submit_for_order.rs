use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "APPROVED_SUBMIT_FOR_ORDER"
)]
pub enum APPROVED_SUBMIT_FOR_ORDER {
    #[sea_orm(string_value = "NO")]
    No,
    #[sea_orm(string_value = "WAITING_CONFIRMATION")]
    WaitingConfirmation,
    #[sea_orm(string_value = "YES")]
    Yes,
}
