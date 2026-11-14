use super::process_manager_outbox_status::ProcessManagerOutboxStatus;
use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "ProcessManagerOutbox")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "processName")]
    pub process_name: String,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(column_name = "processKey")]
    pub process_key: String,
    #[sea_orm(column_name = "tenantId")]
    pub tenant_id: String,
    #[sea_orm(column_name = "userId")]
    pub user_id: Option<String>,
    #[sea_orm(column_name = "messageKey")]
    pub message_key: String,
    #[sea_orm(column_name = "intentType")]
    pub intent_type: String,
    pub payload: Json,
    #[sea_orm(column_name = "traceCarrier")]
    pub trace_carrier: Json,
    #[sea_orm(column_name = "sourceEventId")]
    pub source_event_id: Option<String>,
    #[sea_orm(default_value = "pending")]
    pub status: ProcessManagerOutboxStatus,
    #[sea_orm(default_value = 0)]
    pub attempts: i32,
    #[sea_orm(column_name = "nextAttemptAt", column_type = "TimestampWithTimeZone")]
    pub next_attempt_at: DateTimeWithTimeZone,
    #[sea_orm(column_name = "leasedUntil", column_type = "TimestampWithTimeZone")]
    pub leased_until: Option<DateTimeWithTimeZone>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "leaseToken")]
    pub lease_token: Option<String>,
    #[sea_orm(column_name = "dispatchedAt", column_type = "TimestampWithTimeZone")]
    pub dispatched_at: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_name = "createdAt", column_type = "TimestampWithTimeZone")]
    pub created_at: DateTimeWithTimeZone,
    #[sea_orm(column_name = "updatedAt", column_type = "TimestampWithTimeZone")]
    pub updated_at: DateTimeWithTimeZone,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::process_manager_outbox_attempt::Entity")]
    AttemptLog,
}

impl Related<super::process_manager_outbox_attempt::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AttemptLog.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
