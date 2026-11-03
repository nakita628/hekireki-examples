use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "ScheduleGeneratorType"
)]
pub enum ScheduleGeneratorType {
    #[sea_orm(string_value = "CRON")]
    Cron,
}
