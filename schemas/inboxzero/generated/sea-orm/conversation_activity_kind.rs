use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "ConversationActivityKind"
)]
pub enum ConversationActivityKind {
    #[sea_orm(string_value = "INVITED")]
    Invited,
    #[sea_orm(string_value = "COMMENT")]
    Comment,
    #[sea_orm(string_value = "MENTION")]
    Mention,
}
