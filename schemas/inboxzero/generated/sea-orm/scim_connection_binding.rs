use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "scimConnectionBinding")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "connectionId")]
    pub connection_id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "connectionKey")]
    pub connection_key: String,
    #[sea_orm(column_name = "provisioningDomainId")]
    pub provisioning_domain_id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "decommissionedAt")]
    pub decommissioned_at: Option<DateTime>,
    #[sea_orm(column_name = "decommissionStatus", default_value = "active")]
    pub decommission_status: String,
    #[sea_orm(column_name = "decommissionCursorUserId")]
    pub decommission_cursor_user_id: Option<String>,
    #[sea_orm(column_name = "decommissionReconciledUserCount", default_value = 0)]
    pub decommission_reconciled_user_count: i32,
    #[sea_orm(column_name = "decommissionBatchCount", default_value = 0)]
    pub decommission_batch_count: i32,
    #[sea_orm(column_name = "decommissionRevision", default_value = 0)]
    pub decommission_revision: i32,
    #[sea_orm(column_name = "decommissionCompletedAt")]
    pub decommission_completed_at: Option<DateTime>,
    #[sea_orm(column_name = "decommissionLeaseId")]
    pub decommission_lease_id: Option<String>,
    #[sea_orm(column_name = "decommissionLeaseExpiresAt")]
    pub decommission_lease_expires_at: Option<DateTime>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

impl ActiveModelBehavior for ActiveModel {}
