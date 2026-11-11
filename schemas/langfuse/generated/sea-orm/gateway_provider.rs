use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "GatewayProvider")]
pub enum GatewayProvider {
    #[sea_orm(string_value = "openai")]
    Openai,
    #[sea_orm(string_value = "anthropic")]
    Anthropic,
    #[sea_orm(string_value = "bedrock")]
    Bedrock,
    #[sea_orm(string_value = "openrouter")]
    Openrouter,
    #[sea_orm(string_value = "xai")]
    Xai,
    #[sea_orm(string_value = "vercel")]
    Vercel,
}
