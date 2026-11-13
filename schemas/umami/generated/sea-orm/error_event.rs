use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "error_event")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_type = "Uuid")]
    pub website_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "event_id", column_type = "Uuid")]
    pub id: String,
    #[sea_orm(column_type = "Uuid")]
    pub issue_id: String,
    #[sea_orm(column_type = "Uuid")]
    pub session_id: String,
    #[sea_orm(column_type = "Uuid")]
    pub visit_id: String,
    #[sea_orm(column_type = "TimestampWithTimeZone")]
    pub created_at: DateTimeWithTimeZone,
    #[sea_orm(column_type = "TimestampWithTimeZone")]
    pub received_at: DateTimeWithTimeZone,
    #[sea_orm(column_type = "String(StringLen::N(200))")]
    pub name: String,
    #[sea_orm(column_type = "String(StringLen::N(2000))")]
    pub message: String,
    #[sea_orm(column_type = "Text")]
    pub stack: String,
    pub frames: Json,
    pub tags: Json,
    pub handled: bool,
    #[sea_orm(column_type = "String(StringLen::N(500))")]
    pub url_path: String,
    #[sea_orm(column_type = "String(StringLen::N(100))")]
    pub release: String,
    #[sea_orm(column_type = "String(StringLen::N(50))")]
    pub environment: String,
    #[sea_orm(column_type = "String(StringLen::N(50))")]
    pub browser: String,
    #[sea_orm(column_type = "String(StringLen::N(50))")]
    pub os: String,
    #[sea_orm(column_type = "String(StringLen::N(50))")]
    pub device: String,
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
        if insert && self.received_at.is_not_set() {
            self.received_at = Set(now.fixed_offset());
        }
        Ok(self)
    }
}
