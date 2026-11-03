use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "EmailType")]
pub enum EmailType {
    #[sea_orm(string_value = "FIRST_DAY_DOMAIN_REMINDER_EMAIL")]
    FirstDayDomainReminderEmail,
    #[sea_orm(string_value = "FIRST_DOMAIN_INVALID_EMAIL")]
    FirstDomainInvalidEmail,
    #[sea_orm(string_value = "SECOND_DOMAIN_INVALID_EMAIL")]
    SecondDomainInvalidEmail,
    #[sea_orm(string_value = "FIRST_TRIAL_END_REMINDER_EMAIL")]
    FirstTrialEndReminderEmail,
    #[sea_orm(string_value = "FINAL_TRIAL_END_REMINDER_EMAIL")]
    FinalTrialEndReminderEmail,
}
