use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "view")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: String,
    pub description: Option<String>,
    pub table_id: String,
    pub r#type: String,
    pub sort: Option<String>,
    pub filter: Option<String>,
    pub group: Option<String>,
    pub options: Option<String>,
    pub order: f64,
    pub version: i32,
    pub column_meta: String,
    pub is_locked: Option<bool>,
    pub enable_share: Option<bool>,
    #[sea_orm(unique)]
    pub share_id: Option<String>,
    pub share_meta: Option<String>,
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
