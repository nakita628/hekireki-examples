use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "FieldType")]
pub enum FieldType {
    #[sea_orm(string_value = "SIGNATURE")]
    Signature,
    #[sea_orm(string_value = "FREE_SIGNATURE")]
    FreeSignature,
    #[sea_orm(string_value = "INITIALS")]
    Initials,
    #[sea_orm(string_value = "NAME")]
    Name,
    #[sea_orm(string_value = "EMAIL")]
    Email,
    #[sea_orm(string_value = "DATE")]
    Date,
    #[sea_orm(string_value = "TEXT")]
    Text,
    #[sea_orm(string_value = "NUMBER")]
    Number,
    #[sea_orm(string_value = "RADIO")]
    Radio,
    #[sea_orm(string_value = "CHECKBOX")]
    Checkbox,
    #[sea_orm(string_value = "DROPDOWN")]
    Dropdown,
}
