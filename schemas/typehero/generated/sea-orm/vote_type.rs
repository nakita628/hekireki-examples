use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "VoteType")]
pub enum VoteType {
    #[sea_orm(string_value = "CHALLENGE")]
    Challenge,
    #[sea_orm(string_value = "COMMENT")]
    Comment,
    #[sea_orm(string_value = "SUBMISSION")]
    Submission,
    #[sea_orm(string_value = "SHAREDSOLUTION")]
    Sharedsolution,
}
