use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "share")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "share_id", column_type = "Uuid")]
    pub id: String,
    #[sea_orm(column_type = "Uuid")]
    pub entity_id: String,
    #[sea_orm(column_type = "String(StringLen::N(200))")]
    pub name: String,
    pub share_type: i32,
    #[sea_orm(unique)]
    #[sea_orm(column_type = "String(StringLen::N(100))")]
    pub slug: String,
    pub parameters: Json,
    #[sea_orm(column_type = "TimestampWithTimeZone")]
    pub created_at: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_type = "TimestampWithTimeZone")]
    pub updated_at: Option<DateTimeWithTimeZone>,
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
            self.created_at = Set(Some(now.fixed_offset()));
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(Some(now.fixed_offset()));
        }
        Ok(self)
    }
}
