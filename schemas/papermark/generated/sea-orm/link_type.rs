use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "LinkType")]
pub enum LinkType {
    #[sea_orm(string_value = "DOCUMENT_LINK")]
    DocumentLink,
    #[sea_orm(string_value = "DATAROOM_LINK")]
    DataroomLink,
    #[sea_orm(string_value = "WORKFLOW_LINK")]
    WorkflowLink,
}
