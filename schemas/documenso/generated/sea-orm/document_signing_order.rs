use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "DocumentSigningOrder"
)]
pub enum DocumentSigningOrder {
    #[sea_orm(string_value = "PARALLEL")]
    Parallel,
    #[sea_orm(string_value = "SEQUENTIAL")]
    Sequential,
}
