use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "SystemRole")]
pub enum SystemRole {
    #[sea_orm(string_value = "OWNER")]
    Owner,
    #[sea_orm(string_value = "ADMIN")]
    Admin,
    #[sea_orm(string_value = "MEMBER")]
    Member,
    #[sea_orm(string_value = "VIEWER")]
    Viewer,
    #[sea_orm(string_value = "NONE")]
    None,
    #[sea_orm(string_value = "LEGACY_PROJECT_API_KEY")]
    LegacyProjectApiKey,
    #[sea_orm(string_value = "LEGACY_ORGANIZATION_API_KEY")]
    LegacyOrganizationApiKey,
    #[sea_orm(string_value = "SCORES_INGEST")]
    ScoresIngest,
    #[sea_orm(string_value = "INGEST")]
    Ingest,
    #[sea_orm(string_value = "AI_GATEWAY")]
    AiGateway,
}
