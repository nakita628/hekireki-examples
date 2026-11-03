use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "MarketDataState")]
pub enum MarketDataState {
    #[sea_orm(string_value = "CLOSE")]
    Close,
    #[sea_orm(string_value = "INTRADAY")]
    Intraday,
}
