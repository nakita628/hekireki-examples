use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "RedirectType")]
pub enum RedirectType {
    #[sea_orm(string_value = "user-event-type")]
    Usereventtype,
    #[sea_orm(string_value = "team-event-type")]
    Teameventtype,
    #[sea_orm(string_value = "user")]
    User,
    #[sea_orm(string_value = "team")]
    Team,
}
