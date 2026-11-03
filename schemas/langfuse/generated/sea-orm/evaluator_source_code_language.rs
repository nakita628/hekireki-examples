use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "EvaluatorSourceCodeLanguage"
)]
pub enum EvaluatorSourceCodeLanguage {
    #[sea_orm(string_value = "PYTHON")]
    Python,
    #[sea_orm(string_value = "TYPESCRIPT")]
    Typescript,
}
