use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "BookingLinkLocationType"
)]
pub enum BookingLinkLocationType {
    #[sea_orm(string_value = "IN_PERSON")]
    InPerson,
    #[sea_orm(string_value = "GOOGLE_MEET")]
    GoogleMeet,
    #[sea_orm(string_value = "MICROSOFT_TEAMS")]
    MicrosoftTeams,
    #[sea_orm(string_value = "PHONE")]
    Phone,
    #[sea_orm(string_value = "CUSTOM")]
    Custom,
}
