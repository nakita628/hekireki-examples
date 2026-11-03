use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "SigningStatus")]
pub enum SigningStatus {
    #[sea_orm(string_value = "NOT_SIGNED")]
    NotSigned,
    #[sea_orm(string_value = "SIGNED")]
    Signed,
    #[sea_orm(string_value = "REJECTED")]
    Rejected,
}
