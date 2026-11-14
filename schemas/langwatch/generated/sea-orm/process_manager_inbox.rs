use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "ProcessManagerInbox")]
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
    #[sea_orm(column_name = "sourceEventId")]
    pub source_event_id: String,
    #[sea_orm(column_name = "sourceEventKey")]
    pub source_event_key: Option<String>,
    #[sea_orm(column_name = "consumedAt", column_type = "TimestampWithTimeZone")]
    pub consumed_at: DateTimeWithTimeZone,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

impl ActiveModelBehavior for ActiveModel {}
