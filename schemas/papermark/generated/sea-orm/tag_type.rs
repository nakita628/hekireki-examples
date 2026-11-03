use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "TagType")]
pub enum TagType {
    #[sea_orm(string_value = "LINK_TAG")]
    LinkTag,
    #[sea_orm(string_value = "DOCUMENT_TAG")]
    DocumentTag,
    #[sea_orm(string_value = "DATAROOM_TAG")]
    DataroomTag,
}
