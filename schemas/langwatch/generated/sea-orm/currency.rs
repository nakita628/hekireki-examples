use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "Currency")]
pub enum Currency {
    #[sea_orm(string_value = "USD")]
    Usd,
    #[sea_orm(string_value = "EUR")]
    Eur,
}
