use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "ScimSyncState")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "connectionId")]
    pub connection_id: String,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    pub state: String,
    #[sea_orm(column_name = "lastPushedAt")]
    pub last_pushed_at: Option<DateTime>,
    #[sea_orm(column_name = "lastFailure")]
    pub last_failure: Option<Json>,
    #[sea_orm(column_name = "deadLetters", default_value = "[]")]
    pub dead_letters: Json,
    #[sea_orm(column_name = "revokedCause")]
    pub revoked_cause: Option<String>,
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
