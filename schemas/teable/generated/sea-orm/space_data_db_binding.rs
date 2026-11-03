use super::space_data_db_binding_mode::SpaceDataDbBindingMode;
use super::space_data_db_binding_state::SpaceDataDbBindingState;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "space_data_db_binding")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    pub space_id: String,
    pub data_db_connection_id: Option<String>,
    #[sea_orm(default_value = "default")]
    pub mode: SpaceDataDbBindingMode,
    #[sea_orm(default_value = "ready")]
    pub state: SpaceDataDbBindingState,
    pub created_by: String,
    pub created_time: DateTime,
    pub last_modified_time: Option<DateTime>,
    pub last_history_flushed_at: Option<DateTime>,
    pub last_removal_flushed_at: Option<DateTime>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::space::Entity",
        from = "Column::SpaceId",
        to = "super::space::Column::Id",
        on_delete = "Cascade"
    )]
    Space,
    #[sea_orm(
        belongs_to = "super::data_db_connection::Entity",
        from = "Column::DataDbConnectionId",
        to = "super::data_db_connection::Column::Id"
    )]
    DataDbConnection,
}

impl Related<super::space::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Space.def()
    }
}

impl Related<super::data_db_connection::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DataDbConnection.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_time.is_not_set() {
            self.created_time = Set(now.naive_utc());
        }
        if !self.last_modified_time.is_set() {
            self.last_modified_time = Set(Some(now.naive_utc()));
        }
        Ok(self)
    }
}
