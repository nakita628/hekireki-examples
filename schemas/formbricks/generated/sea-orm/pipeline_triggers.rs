use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "PipelineTriggers")]
pub enum PipelineTriggers {
    #[sea_orm(string_value = "responseCreated")]
    Responsecreated,
    #[sea_orm(string_value = "responseUpdated")]
    Responseupdated,
    #[sea_orm(string_value = "responseFinished")]
    Responsefinished,
}
