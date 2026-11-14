use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "ProcessManagerInstance")]
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
    pub state: Json,
    pub revision: i32,
    #[sea_orm(column_name = "nextWakeAt", column_type = "TimestampWithTimeZone")]
    pub next_wake_at: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_name = "updatedAt", column_type = "TimestampWithTimeZone")]
    pub updated_at: DateTimeWithTimeZone,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

impl ActiveModelBehavior for ActiveModel {}
