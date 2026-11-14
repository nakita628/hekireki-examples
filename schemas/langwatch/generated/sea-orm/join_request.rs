use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "JoinRequest")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "userId")]
    pub user_id: String,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    pub domain: String,
    pub state: String,
    #[sea_orm(column_name = "matchedVia")]
    pub matched_via: String,
    #[sea_orm(default_value = "web")]
    pub origin: String,
    #[sea_orm(column_name = "expiresAt")]
    pub expires_at: Option<DateTime>,
    #[sea_orm(column_name = "resolvedAt")]
    pub resolved_at: Option<DateTime>,
    #[sea_orm(column_name = "resolvedByType")]
    pub resolved_by_type: Option<String>,
    #[sea_orm(column_name = "resolvedById")]
    pub resolved_by_id: Option<String>,
    #[sea_orm(column_name = "withdrawalCause")]
    pub withdrawal_cause: Option<String>,
    #[sea_orm(column_name = "connectionId")]
    pub connection_id: Option<String>,
    #[sea_orm(column_name = "occurredAt")]
    pub occurred_at: DateTime,
    #[sea_orm(column_name = "lastEventId")]
    pub last_event_id: String,
    #[sea_orm(column_name = "acceptedAt")]
    pub accepted_at: DateTime,
    #[sea_orm(column_name = "projectionVersion")]
    pub projection_version: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

impl ActiveModelBehavior for ActiveModel {}
