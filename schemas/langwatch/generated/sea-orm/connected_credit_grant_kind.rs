use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "ConnectedCreditGrantKind"
)]
pub enum ConnectedCreditGrantKind {
    #[sea_orm(string_value = "commit")]
    Commit,
    #[sea_orm(string_value = "added")]
    Added,
    #[sea_orm(string_value = "renewal")]
    Renewal,
}
