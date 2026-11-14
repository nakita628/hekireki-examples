use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "ConnectedInvoiceKind"
)]
pub enum ConnectedInvoiceKind {
    #[sea_orm(string_value = "annual")]
    Annual,
    #[sea_orm(string_value = "seat_change")]
    SeatChange,
    #[sea_orm(string_value = "usage")]
    Usage,
}
