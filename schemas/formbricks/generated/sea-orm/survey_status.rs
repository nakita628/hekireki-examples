use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "SurveyStatus")]
pub enum SurveyStatus {
    #[sea_orm(string_value = "draft")]
    Draft,
    #[sea_orm(string_value = "inProgress")]
    Inprogress,
    #[sea_orm(string_value = "paused")]
    Paused,
    #[sea_orm(string_value = "completed")]
    Completed,
}
