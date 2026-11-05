use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "scimManagedConnection")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "creationRequestId")]
    pub creation_request_id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "connectionId")]
    pub connection_id: String,
    #[sea_orm(column_name = "provisioningDomainId")]
    pub provisioning_domain_id: String,
    pub status: String,
    pub revision: i32,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "createdBy")]
    pub created_by: String,
    #[sea_orm(column_name = "decommissionStartedAt")]
    pub decommission_started_at: Option<DateTime>,
    #[sea_orm(column_name = "decommissionStartedBy")]
    pub decommission_started_by: Option<String>,
    #[sea_orm(column_name = "decommissionedAt")]
    pub decommissioned_at: Option<DateTime>,
    #[sea_orm(column_name = "decommissionedBy")]
    pub decommissioned_by: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::scim_managed_credential::Entity")]
    Scimmanagedcredentials,
    #[sea_orm(has_many = "super::scim_managed_connection_event::Entity")]
    Scimmanagedconnectionevents,
}

impl Related<super::scim_managed_credential::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Scimmanagedcredentials.def()
    }
}

impl Related<super::scim_managed_connection_event::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Scimmanagedconnectionevents.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
