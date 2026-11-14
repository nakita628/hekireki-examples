use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "AnnotationScoreDataType"
)]
pub enum AnnotationScoreDataType {
    #[sea_orm(string_value = "CATEGORICAL")]
    Categorical,
    #[sea_orm(string_value = "BOOLEAN")]
    Boolean,
    #[sea_orm(string_value = "LIKERT")]
    Likert,
    #[sea_orm(string_value = "OPTION")]
    Option,
    #[sea_orm(string_value = "CHECKBOX")]
    Checkbox,
}
