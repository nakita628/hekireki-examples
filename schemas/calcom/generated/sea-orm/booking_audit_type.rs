use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "BookingAuditType")]
pub enum BookingAuditType {
    #[sea_orm(string_value = "record_created")]
    RecordCreated,
    #[sea_orm(string_value = "record_updated")]
    RecordUpdated,
    #[sea_orm(string_value = "record_deleted")]
    RecordDeleted,
}
