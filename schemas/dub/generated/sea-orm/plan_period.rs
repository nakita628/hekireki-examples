use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "PlanPeriod")]
pub enum PlanPeriod {
    #[sea_orm(string_value = "monthly")]
    Monthly,
    #[sea_orm(string_value = "yearly")]
    Yearly,
    #[sea_orm(string_value = "quarterly")]
    Quarterly,
    #[sea_orm(string_value = "biweekly")]
    Biweekly,
}
