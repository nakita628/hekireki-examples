use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "vote_type")]
pub enum VoteType {
    #[sea_orm(string_value = "yes")]
    Yes,
    #[sea_orm(string_value = "no")]
    No,
    #[sea_orm(string_value = "ifNeedBe")]
    Ifneedbe,
}
