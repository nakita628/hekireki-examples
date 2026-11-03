use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "DataSource")]
pub enum DataSource {
    #[sea_orm(string_value = "ALPHA_VANTAGE")]
    AlphaVantage,
    #[sea_orm(string_value = "COINGECKO")]
    Coingecko,
    #[sea_orm(string_value = "EOD_HISTORICAL_DATA")]
    EodHistoricalData,
    #[sea_orm(string_value = "FINANCIAL_MODELING_PREP")]
    FinancialModelingPrep,
    #[sea_orm(string_value = "GHOSTFOLIO")]
    Ghostfolio,
    #[sea_orm(string_value = "GOOGLE_SHEETS")]
    GoogleSheets,
    #[sea_orm(string_value = "MANUAL")]
    Manual,
    #[sea_orm(string_value = "RAPID_API")]
    RapidApi,
    #[sea_orm(string_value = "YAHOO")]
    Yahoo,
}
