use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "MessagingRoutePurpose"
)]
pub enum MessagingRoutePurpose {
    #[sea_orm(string_value = "RULE_NOTIFICATIONS")]
    RuleNotifications,
    #[sea_orm(string_value = "SCHEDULED_CHECK_INS")]
    ScheduledCheckIns,
    #[sea_orm(string_value = "MEETING_BRIEFS")]
    MeetingBriefs,
    #[sea_orm(string_value = "DOCUMENT_FILINGS")]
    DocumentFilings,
    #[sea_orm(string_value = "DIGESTS")]
    Digests,
    #[sea_orm(string_value = "FOLLOW_UPS")]
    FollowUps,
}
