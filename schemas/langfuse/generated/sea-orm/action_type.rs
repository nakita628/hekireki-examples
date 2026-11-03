use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "ActionType")]
pub enum ActionType {
    #[sea_orm(string_value = "WEBHOOK")]
    Webhook,
    #[sea_orm(string_value = "SLACK")]
    Slack,
    #[sea_orm(string_value = "GITHUB_DISPATCH")]
    GithubDispatch,
    #[sea_orm(string_value = "PAGERDUTY")]
    Pagerduty,
}
