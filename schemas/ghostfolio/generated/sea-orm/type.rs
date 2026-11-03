use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "Type")]
pub enum Type {
    #[sea_orm(string_value = "BUY")]
    Buy,
    #[sea_orm(string_value = "DIVIDEND")]
    Dividend,
    #[sea_orm(string_value = "FEE")]
    Fee,
    #[sea_orm(string_value = "INTEREST")]
    Interest,
    #[sea_orm(string_value = "LIABILITY")]
    Liability,
    #[sea_orm(string_value = "SELL")]
    Sell,
}
