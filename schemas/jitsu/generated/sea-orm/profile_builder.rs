use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "ProfileBuilder")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: String,
    #[sea_orm(default_value = false)]
    pub deleted: Option<bool>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "workspaceId")]
    pub workspace_id: String,
    #[sea_orm(column_name = "intermediateStorageCredentials")]
    pub intermediate_storage_credentials: Json,
    #[sea_orm(column_name = "destinationId")]
    pub destination_id: Option<String>,
    #[sea_orm(column_name = "connectionOptions")]
    pub connection_options: Option<Json>,
    #[sea_orm(default_value = 0)]
    pub version: i32,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::workspace::Entity",
        from = "Column::WorkspaceId",
        to = "super::workspace::Column::Id"
    )]
    Workspace,
    #[sea_orm(
        belongs_to = "super::configuration_object::Entity",
        from = "Column::DestinationId",
        to = "super::configuration_object::Column::Id"
    )]
    Destination,
    #[sea_orm(has_many = "super::profile_builder_function::Entity")]
    Functions,
}

impl Related<super::workspace::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Workspace.def()
    }
}

impl Related<super::configuration_object::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Destination.def()
    }
}

impl Related<super::profile_builder_function::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Functions.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.naive_utc());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
