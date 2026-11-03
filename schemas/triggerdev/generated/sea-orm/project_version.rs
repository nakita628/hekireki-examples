use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "ProjectVersion")]
pub enum ProjectVersion {
    #[sea_orm(string_value = "V2")]
    V2,
    #[sea_orm(string_value = "V3")]
    V3,
}
