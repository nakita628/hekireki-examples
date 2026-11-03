use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "EventTypeAutoTranslatedField"
)]
pub enum EventTypeAutoTranslatedField {
    #[sea_orm(string_value = "DESCRIPTION")]
    Description,
    #[sea_orm(string_value = "TITLE")]
    Title,
}
