use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "CustomFieldType")]
pub enum CustomFieldType {
    #[sea_orm(string_value = "SHORT_TEXT")]
    ShortText,
    #[sea_orm(string_value = "LONG_TEXT")]
    LongText,
    #[sea_orm(string_value = "NUMBER")]
    Number,
    #[sea_orm(string_value = "PHONE_NUMBER")]
    PhoneNumber,
    #[sea_orm(string_value = "URL")]
    Url,
    #[sea_orm(string_value = "CHECKBOX")]
    Checkbox,
    #[sea_orm(string_value = "SELECT")]
    Select,
    #[sea_orm(string_value = "MULTI_SELECT")]
    MultiSelect,
}
