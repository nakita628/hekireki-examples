use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "ChatProvider")]
pub enum ChatProvider {
    #[sea_orm(string_value = "WHATSAPP")]
    Whatsapp,
    #[sea_orm(string_value = "DIALOG360")]
    Dialog360,
}
