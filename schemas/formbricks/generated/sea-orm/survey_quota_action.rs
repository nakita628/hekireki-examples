use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "SurveyQuotaAction")]
pub enum SurveyQuotaAction {
    #[sea_orm(string_value = "endSurvey")]
    Endsurvey,
    #[sea_orm(string_value = "continueSurvey")]
    Continuesurvey,
}
