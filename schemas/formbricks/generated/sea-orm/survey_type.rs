use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "SurveyType")]
pub enum SurveyType {
    #[sea_orm(string_value = "link")]
    Link,
    #[sea_orm(string_value = "app")]
    App,
}
