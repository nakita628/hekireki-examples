use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "ClassificationFeedbackEventType"
)]
pub enum ClassificationFeedbackEventType {
    #[sea_orm(string_value = "LABEL_ADDED")]
    LabelAdded,
    #[sea_orm(string_value = "LABEL_REMOVED")]
    LabelRemoved,
}
