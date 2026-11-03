use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "LicenseType")]
pub enum LicenseType {
    #[sea_orm(string_value = "PLUS")]
    Plus,
    #[sea_orm(string_value = "ORGANIZATION")]
    Organization,
    #[sea_orm(string_value = "ENTERPRISE")]
    Enterprise,
}
