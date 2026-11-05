use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "scimManagedConnectionEvent")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "connectionRecordId")]
    pub connection_record_id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "eventKey")]
    pub event_key: String,
    pub sequence: i32,
    pub r#type: String,
    #[sea_orm(column_name = "actorId")]
    pub actor_id: String,
    #[sea_orm(column_name = "credentialId")]
    pub credential_id: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::scim_managed_connection::Entity",
        from = "Column::ConnectionRecordId",
        to = "super::scim_managed_connection::Column::Id",
        on_delete = "Cascade"
    )]
    Scimmanagedconnection,
}

impl Related<super::scim_managed_connection::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Scimmanagedconnection.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
