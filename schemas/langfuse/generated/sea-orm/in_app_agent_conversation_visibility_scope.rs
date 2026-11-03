use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "InAppAgentConversationVisibilityScope"
)]
pub enum InAppAgentConversationVisibilityScope {
    #[sea_orm(string_value = "PERSONAL")]
    Personal,
    #[sea_orm(string_value = "PROJECT")]
    Project,
}
