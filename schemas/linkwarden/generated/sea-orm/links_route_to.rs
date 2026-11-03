use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "LinksRouteTo")]
pub enum LinksRouteTo {
    #[sea_orm(string_value = "ORIGINAL")]
    Original,
    #[sea_orm(string_value = "PDF")]
    Pdf,
    #[sea_orm(string_value = "READABLE")]
    Readable,
    #[sea_orm(string_value = "MONOLITH")]
    Monolith,
    #[sea_orm(string_value = "SCREENSHOT")]
    Screenshot,
    #[sea_orm(string_value = "DETAILS")]
    Details,
}
