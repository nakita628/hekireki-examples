use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "DocumentSource")]
pub enum DocumentSource {
    #[sea_orm(string_value = "DOCUMENT")]
    Document,
    #[sea_orm(string_value = "TEMPLATE")]
    Template,
    #[sea_orm(string_value = "TEMPLATE_DIRECT_LINK")]
    TemplateDirectLink,
}
