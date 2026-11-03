use super::provision_state::ProvisionState;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "field")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: String,
    pub description: Option<String>,
    pub options: Option<String>,
    pub meta: Option<String>,
    pub ai_config: Option<String>,
    pub r#type: String,
    pub cell_value_type: String,
    pub is_multiple_cell_value: Option<bool>,
    pub db_field_type: String,
    pub db_field_name: String,
    #[sea_orm(default_value = "ready")]
    pub provision_state: ProvisionState,
    pub not_null: Option<bool>,
    pub unique: Option<bool>,
    pub is_primary: Option<bool>,
    pub is_computed: Option<bool>,
    pub is_lookup: Option<bool>,
    pub is_conditional_lookup: Option<bool>,
    pub is_pending: Option<bool>,
    pub has_error: Option<bool>,
    pub lookup_linked_field_id: Option<String>,
    pub lookup_options: Option<String>,
    pub table_id: String,
    pub order: f64,
    pub version: i32,
    pub created_time: DateTime,
    pub last_modified_time: Option<DateTime>,
    pub deleted_time: Option<DateTime>,
    pub created_by: String,
    pub last_modified_by: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::table_meta::Entity",
        from = "Column::TableId",
        to = "super::table_meta::Column::Id"
    )]
    Table,
}

impl Related<super::table_meta::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Table.def()
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
