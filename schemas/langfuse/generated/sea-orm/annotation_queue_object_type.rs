use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "AnnotationQueueObjectType"
)]
pub enum AnnotationQueueObjectType {
    #[sea_orm(string_value = "TRACE")]
    Trace,
    #[sea_orm(string_value = "OBSERVATION")]
    Observation,
    #[sea_orm(string_value = "SESSION")]
    Session,
}
