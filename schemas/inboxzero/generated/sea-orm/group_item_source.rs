use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "GroupItemSource")]
pub enum GroupItemSource {
    #[sea_orm(string_value = "AI")]
    Ai,
    #[sea_orm(string_value = "USER")]
    User,
    #[sea_orm(string_value = "LABEL_REMOVED")]
    LabelRemoved,
    #[sea_orm(string_value = "LABEL_ADDED")]
    LabelAdded,
}
