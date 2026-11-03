use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "plugin_context_menu")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub table_id: String,
    #[sea_orm(unique)]
    pub plugin_install_id: String,
    pub order: f64,
    pub created_time: DateTime,
    pub created_by: String,
    pub last_modified_time: Option<DateTime>,
    pub last_modified_by: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::table_meta::Entity",
        from = "Column::TableId",
        to = "super::table_meta::Column::Id",
        on_delete = "Cascade"
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
