use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "AssetSubClass")]
pub enum AssetSubClass {
    #[sea_orm(string_value = "BOND")]
    Bond,
    #[sea_orm(string_value = "CASH")]
    Cash,
    #[sea_orm(string_value = "COLLECTIBLE")]
    Collectible,
    #[sea_orm(string_value = "COMMODITY")]
    Commodity,
    #[sea_orm(string_value = "CRYPTOCURRENCY")]
    Cryptocurrency,
    #[sea_orm(string_value = "ETF")]
    Etf,
    #[sea_orm(string_value = "LOAN")]
    Loan,
    #[sea_orm(string_value = "MUTUALFUND")]
    Mutualfund,
    #[sea_orm(string_value = "PRECIOUS_METAL")]
    PreciousMetal,
    #[sea_orm(string_value = "PRIVATE_EQUITY")]
    PrivateEquity,
    #[sea_orm(string_value = "STOCK")]
    Stock,
}
