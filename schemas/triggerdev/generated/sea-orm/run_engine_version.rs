use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "RunEngineVersion")]
pub enum RunEngineVersion {
    #[sea_orm(string_value = "V1")]
    V1,
    #[sea_orm(string_value = "V2")]
    V2,
}
