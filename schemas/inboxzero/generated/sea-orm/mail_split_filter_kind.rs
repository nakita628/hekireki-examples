use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "MailSplitFilterKind"
)]
pub enum MailSplitFilterKind {
    #[sea_orm(string_value = "UNREAD")]
    Unread,
    #[sea_orm(string_value = "STARRED")]
    Starred,
    #[sea_orm(string_value = "LABEL")]
    Label,
    #[sea_orm(string_value = "CATEGORY")]
    Category,
    #[sea_orm(string_value = "FROM")]
    From,
    #[sea_orm(string_value = "OLDER_THAN")]
    OlderThan,
}
