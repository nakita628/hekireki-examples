use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "IssueType")]
pub enum IssueType {
    #[sea_orm(string_value = "DEROGATORY")]
    Derogatory,
    #[sea_orm(string_value = "OTHER")]
    Other,
    #[sea_orm(string_value = "UNCLEAR")]
    Unclear,
    #[sea_orm(string_value = "BULLYING")]
    Bullying,
    #[sea_orm(string_value = "SPAM")]
    Spam,
    #[sea_orm(string_value = "HATESPEECH")]
    Hatespeech,
    #[sea_orm(string_value = "THREAT")]
    Threat,
}
