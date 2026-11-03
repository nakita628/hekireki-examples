use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "RecipientRole")]
pub enum RecipientRole {
    #[sea_orm(string_value = "CC")]
    Cc,
    #[sea_orm(string_value = "SIGNER")]
    Signer,
    #[sea_orm(string_value = "VIEWER")]
    Viewer,
    #[sea_orm(string_value = "APPROVER")]
    Approver,
    #[sea_orm(string_value = "ASSISTANT")]
    Assistant,
}
