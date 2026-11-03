use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "session")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "session_id", column_type = "Uuid")]
    pub id: String,
    #[sea_orm(column_type = "Uuid")]
    pub website_id: String,
    #[sea_orm(column_type = "String(StringLen::N(20))")]
    pub browser: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(20))")]
    pub os: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(20))")]
    pub device: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(11))")]
    pub screen: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(35))")]
    pub language: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(2))")]
    pub country: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(20))")]
    pub region: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(50))")]
    pub city: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(50))")]
    pub distinct_id: Option<String>,
    #[sea_orm(column_type = "TimestampWithTimeZone")]
    pub created_at: Option<DateTimeWithTimeZone>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::website_event::Entity")]
    WebsiteEvents,
    #[sea_orm(has_many = "super::session_data::Entity")]
    SessionData,
    #[sea_orm(has_many = "super::revenue::Entity")]
    Revenue,
}

impl Related<super::website_event::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WebsiteEvents.def()
    }
}

impl Related<super::session_data::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SessionData.def()
    }
}

impl Related<super::revenue::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Revenue.def()
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
            self.created_at = Set(Some(now.fixed_offset()));
        }
        Ok(self)
    }
}
