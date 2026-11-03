use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "ReqType")]
pub enum ReqType {
    #[sea_orm(string_value = "REST")]
    Rest,
    #[sea_orm(string_value = "GQL")]
    Gql,
}
