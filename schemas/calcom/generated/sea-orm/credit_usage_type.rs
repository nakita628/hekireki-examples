use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "CreditUsageType")]
pub enum CreditUsageType {
    #[sea_orm(string_value = "SMS")]
    Sms,
    #[sea_orm(string_value = "CAL_AI_PHONE_CALL")]
    CalAiPhoneCall,
}
