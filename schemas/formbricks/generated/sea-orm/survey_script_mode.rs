use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "SurveyScriptMode")]
pub enum SurveyScriptMode {
    #[sea_orm(string_value = "add")]
    Add,
    #[sea_orm(string_value = "replace")]
    Replace,
}
