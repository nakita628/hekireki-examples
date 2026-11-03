use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "DeletionCleanupKind"
)]
pub enum DeletionCleanupKind {
    #[sea_orm(string_value = "hubSurvey")]
    Hubsurvey,
    #[sea_orm(string_value = "hubResponses")]
    Hubresponses,
    #[sea_orm(string_value = "storageFiles")]
    Storagefiles,
    #[sea_orm(string_value = "storageSurveyFolder")]
    Storagesurveyfolder,
}
