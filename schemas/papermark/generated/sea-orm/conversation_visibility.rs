use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "ConversationVisibility"
)]
pub enum ConversationVisibility {
    #[sea_orm(string_value = "PRIVATE")]
    Private,
    #[sea_orm(string_value = "PUBLIC_LINK")]
    PublicLink,
    #[sea_orm(string_value = "PUBLIC_GROUP")]
    PublicGroup,
    #[sea_orm(string_value = "PUBLIC_DOCUMENT")]
    PublicDocument,
    #[sea_orm(string_value = "PUBLIC_DATAROOM")]
    PublicDataroom,
}
