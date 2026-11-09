use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "MailSetupState")]
pub enum MailSetupState {
    #[sea_orm(string_value = "PENDING")]
    Pending,
    #[sea_orm(string_value = "SUGGESTED")]
    Suggested,
    #[sea_orm(string_value = "DONE")]
    Done,
}
