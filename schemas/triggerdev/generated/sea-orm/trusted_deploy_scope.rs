use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "TrustedDeployScope")]
pub enum TrustedDeployScope {
    #[sea_orm(string_value = "ANY")]
    Any,
    #[sea_orm(string_value = "BRANCH")]
    Branch,
    #[sea_orm(string_value = "TAG")]
    Tag,
    #[sea_orm(string_value = "ENVIRONMENT")]
    Environment,
}
