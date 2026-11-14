use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "OrganizationSeatPeakHolder")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "holderKind")]
    pub holder_kind: String,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "holderId")]
    pub holder_id: String,
    pub seat: String,
    #[sea_orm(column_name = "lastChangedAt")]
    pub last_changed_at: f64,
    #[sea_orm(column_name = "lastChangeId")]
    pub last_change_id: Option<String>,
    #[sea_orm(column_name = "expiresAt")]
    pub expires_at: Option<f64>,
    #[sea_orm(column_name = "expiredByFold", default_value = false)]
    pub expired_by_fold: bool,
    #[sea_orm(column_name = "suppressedBy")]
    pub suppressed_by: Option<String>,
    #[sea_orm(column_name = "restoredByFold", default_value = false)]
    pub restored_by_fold: bool,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

impl ActiveModelBehavior for ActiveModel {}
