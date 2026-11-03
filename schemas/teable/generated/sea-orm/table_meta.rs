use super::provision_state::ProvisionState;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "table_meta")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub base_id: String,
    pub name: String,
    pub description: Option<String>,
    pub icon: Option<String>,
    pub db_table_name: String,
    pub db_view_name: Option<String>,
    #[sea_orm(column_type = "JsonBinary")]
    pub search_index: Option<Json>,
    #[sea_orm(column_type = "JsonBinary")]
    pub search_index_policy: Option<Json>,
    #[sea_orm(column_type = "JsonBinary")]
    pub query_index_policy: Option<Json>,
    #[sea_orm(default_value = "ready")]
    pub provision_state: ProvisionState,
    pub version: i32,
    pub order: f64,
    pub created_time: DateTime,
    pub last_modified_time: Option<DateTime>,
    pub deleted_time: Option<DateTime>,
    pub created_by: String,
    pub last_modified_by: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::base::Entity",
        from = "Column::BaseId",
        to = "super::base::Column::Id"
    )]
    Base,
    #[sea_orm(has_many = "super::field::Entity")]
    Fields,
    #[sea_orm(has_many = "super::view::Entity")]
    Views,
    #[sea_orm(has_many = "super::plugin_panel::Entity")]
    PluginPanel,
    #[sea_orm(has_many = "super::plugin_context_menu::Entity")]
    PluginContextMenu,
}

impl Related<super::base::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Base.def()
    }
}

impl Related<super::field::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Fields.def()
    }
}

impl Related<super::view::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Views.def()
    }
}

impl Related<super::plugin_panel::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PluginPanel.def()
    }
}

impl Related<super::plugin_context_menu::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PluginContextMenu.def()
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
