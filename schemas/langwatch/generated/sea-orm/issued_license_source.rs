use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "IssuedLicenseSource"
)]
pub enum IssuedLicenseSource {
    #[sea_orm(string_value = "BACKOFFICE")]
    Backoffice,
    #[sea_orm(string_value = "PURCHASE")]
    Purchase,
    #[sea_orm(string_value = "SCRIPT")]
    Script,
    #[sea_orm(string_value = "LEGACY_IMPORT")]
    LegacyImport,
}
