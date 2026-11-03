use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "CancellationReasonRequirement"
)]
pub enum CancellationReasonRequirement {
    #[sea_orm(string_value = "MANDATORY_BOTH")]
    MandatoryBoth,
    #[sea_orm(string_value = "MANDATORY_HOST_ONLY")]
    MandatoryHostOnly,
    #[sea_orm(string_value = "MANDATORY_ATTENDEE_ONLY")]
    MandatoryAttendeeOnly,
    #[sea_orm(string_value = "OPTIONAL_BOTH")]
    OptionalBoth,
}
