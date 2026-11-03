use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "OrganisationGroupType"
)]
pub enum OrganisationGroupType {
    #[sea_orm(string_value = "INTERNAL_ORGANISATION")]
    InternalOrganisation,
    #[sea_orm(string_value = "INTERNAL_TEAM")]
    InternalTeam,
    #[sea_orm(string_value = "CUSTOM")]
    Custom,
}
