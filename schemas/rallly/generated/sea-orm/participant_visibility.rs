use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "participant_visibility"
)]
pub enum ParticipantVisibility {
    #[sea_orm(string_value = "full")]
    Full,
    #[sea_orm(string_value = "scoresOnly")]
    Scoresonly,
    #[sea_orm(string_value = "limited")]
    Limited,
}
