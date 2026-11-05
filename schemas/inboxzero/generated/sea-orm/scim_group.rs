use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "scimGroup")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "connectionId")]
    pub connection_id: String,
    #[sea_orm(column_name = "provisioningDomainId")]
    pub provisioning_domain_id: String,
    #[sea_orm(default_value = 0)]
    pub revision: i32,
    #[sea_orm(column_name = "displayName")]
    pub display_name: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "displayNameKey")]
    pub display_name_key: String,
    #[sea_orm(column_name = "externalId")]
    pub external_id: Option<String>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "externalIdKey")]
    pub external_id_key: Option<String>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "orderKey")]
    pub order_key: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::scim_group_member::Entity")]
    ScimGroupMembers,
}

impl Related<super::scim_group_member::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ScimGroupMembers.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
