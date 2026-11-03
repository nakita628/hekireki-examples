use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "AnalyticsIntegrationExportSource"
)]
pub enum AnalyticsIntegrationExportSource {
    #[sea_orm(string_value = "TRACES_OBSERVATIONS")]
    TracesObservations,
    #[sea_orm(string_value = "TRACES_OBSERVATIONS_EVENTS")]
    TracesObservationsEvents,
    #[sea_orm(string_value = "EVENTS")]
    Events,
}
