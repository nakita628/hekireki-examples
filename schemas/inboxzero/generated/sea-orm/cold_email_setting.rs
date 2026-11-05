use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "ColdEmailSetting")]
pub enum ColdEmailSetting {
    #[sea_orm(string_value = "DISABLED")]
    Disabled,
    #[sea_orm(string_value = "LIST")]
    List,
    #[sea_orm(string_value = "LABEL")]
    Label,
    #[sea_orm(string_value = "ARCHIVE_AND_LABEL")]
    ArchiveAndLabel,
    #[sea_orm(string_value = "ARCHIVE_AND_READ_AND_LABEL")]
    ArchiveAndReadAndLabel,
}
