use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "PremiumTier")]
pub enum PremiumTier {
    #[sea_orm(string_value = "BASIC_MONTHLY")]
    BasicMonthly,
    #[sea_orm(string_value = "BASIC_ANNUALLY")]
    BasicAnnually,
    #[sea_orm(string_value = "PRO_MONTHLY")]
    ProMonthly,
    #[sea_orm(string_value = "PRO_ANNUALLY")]
    ProAnnually,
    #[sea_orm(string_value = "BUSINESS_MONTHLY")]
    StarterMonthly,
    #[sea_orm(string_value = "BUSINESS_ANNUALLY")]
    StarterAnnually,
    #[sea_orm(string_value = "PLUS_MONTHLY")]
    PlusMonthly,
    #[sea_orm(string_value = "PLUS_ANNUALLY")]
    PlusAnnually,
    #[sea_orm(string_value = "BUSINESS_PLUS_MONTHLY")]
    ProfessionalMonthly,
    #[sea_orm(string_value = "BUSINESS_PLUS_ANNUALLY")]
    ProfessionalAnnually,
    #[sea_orm(string_value = "COPILOT_MONTHLY")]
    CopilotMonthly,
    #[sea_orm(string_value = "LIFETIME")]
    Lifetime,
}
