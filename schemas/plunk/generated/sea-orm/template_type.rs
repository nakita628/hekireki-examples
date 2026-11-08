use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "TemplateType")]
pub enum TemplateType {
    #[sea_orm(string_value = "TRANSACTIONAL")]
    Transactional,
    #[sea_orm(string_value = "MARKETING")]
    Marketing,
    #[sea_orm(string_value = "HEADLESS")]
    Headless,
}
