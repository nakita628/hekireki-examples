use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "RetentionSurveyCondition"
)]
pub enum RetentionSurveyCondition {
    #[sea_orm(string_value = "noResponse")]
    Noresponse,
    #[sea_orm(string_value = "noChange")]
    Nochange,
    #[sea_orm(string_value = "createdBefore")]
    Createdbefore,
}
