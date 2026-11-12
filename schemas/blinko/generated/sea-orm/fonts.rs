use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "fonts")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    #[sea_orm(unique)]
    #[sea_orm(column_type = "String(StringLen::N(255))")]
    pub name: String,
    #[sea_orm(column_name = "displayName", column_type = "String(StringLen::N(255))")]
    pub display_name: String,
    #[sea_orm(column_type = "Text")]
    pub url: Option<String>,
    #[sea_orm(column_name = "fileData")]
    pub file_data: Option<Vec<u8>>,
    #[sea_orm(column_name = "isLocal", default_value = false)]
    pub is_local: bool,
    #[sea_orm(column_name = "isSystem", default_value = false)]
    pub is_system: bool,
    pub weights: Json,
    #[sea_orm(column_type = "String(StringLen::N(50))", default_value = "sans-serif")]
    pub category: String,
    #[sea_orm(column_name = "sortOrder", default_value = 0)]
    pub sort_order: i32,
    #[sea_orm(column_name = "createdAt", column_type = "TimestampWithTimeZone")]
    pub created_at: DateTimeWithTimeZone,
    #[sea_orm(column_name = "updatedAt", column_type = "TimestampWithTimeZone")]
    pub updated_at: DateTimeWithTimeZone,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.fixed_offset());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.fixed_offset());
        }
        Ok(self)
    }
}
