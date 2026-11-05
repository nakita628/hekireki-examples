use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "DraftEmailStatus")]
pub enum DraftEmailStatus {
    #[sea_orm(string_value = "PENDING")]
    Pending,
    #[sea_orm(string_value = "LIKELY_SENT")]
    LikelySent,
    #[sea_orm(string_value = "REPLIED_WITHOUT_DRAFT")]
    RepliedWithoutDraft,
    #[sea_orm(string_value = "CLEANED_UP_UNUSED")]
    CleanedUpUnused,
    #[sea_orm(string_value = "MISSING_FROM_PROVIDER")]
    MissingFromProvider,
    #[sea_orm(string_value = "USER_EDITED")]
    UserEdited,
    #[sea_orm(string_value = "CLEANUP_SKIPPED")]
    CleanupSkipped,
}
