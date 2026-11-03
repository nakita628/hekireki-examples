use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "AssetClass")]
pub enum AssetClass {
    #[sea_orm(string_value = "ALTERNATIVE_INVESTMENT")]
    AlternativeInvestment,
    #[sea_orm(string_value = "COMMODITY")]
    Commodity,
    #[sea_orm(string_value = "EQUITY")]
    Equity,
    #[sea_orm(string_value = "FIXED_INCOME")]
    FixedIncome,
    #[sea_orm(string_value = "LIQUIDITY")]
    Liquidity,
    #[sea_orm(string_value = "REAL_ESTATE")]
    RealEstate,
}
