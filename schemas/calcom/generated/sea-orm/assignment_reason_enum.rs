use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "AssignmentReasonEnum"
)]
pub enum AssignmentReasonEnum {
    #[sea_orm(string_value = "REASSIGNED")]
    Reassigned,
    #[sea_orm(string_value = "RR_REASSIGNED")]
    RrReassigned,
    #[sea_orm(string_value = "REROUTED")]
    Rerouted,
    #[sea_orm(string_value = "SALESFORCE_ASSIGNMENT")]
    SalesforceAssignment,
}
