use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "SurveyName")]
pub enum SurveyName {
    #[sea_orm(string_value = "org_onboarding")]
    OrgOnboarding,
    #[sea_orm(string_value = "user_onboarding")]
    UserOnboarding,
}
