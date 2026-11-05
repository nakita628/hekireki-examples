use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "McpTool")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    pub name: String,
    pub description: Option<String>,
    pub schema: Option<Json>,
    #[sea_orm(column_name = "isEnabled", default_value = true)]
    pub is_enabled: bool,
    #[sea_orm(column_name = "isWrite", default_value = false)]
    pub is_write: bool,
    #[sea_orm(column_name = "connectionId")]
    pub connection_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::mcp_connection::Entity",
        from = "Column::ConnectionId",
        to = "super::mcp_connection::Column::Id",
        on_delete = "Cascade"
    )]
    Connection,
}

impl Related<super::mcp_connection::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Connection.def()
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
