use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "CreativeShowOn")]
pub enum CreativeShowOn {
    #[sea_orm(string_value = "pageView")]
    Pageview,
    #[sea_orm(string_value = "action")]
    Action,
}
